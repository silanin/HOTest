using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using HiddenObject.Constants;
using HiddenObject.Enums;
using HiddenObject.Models;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine;

namespace HiddenObject.Services
{
    public interface IStorageService : IService, IDisposable
    {
        void SaveModel(ModelName name, ISaveModel data, bool useEncryption = false);
        ISaveModel LoadModel(ModelName name);
        T LoadModel<T>(ModelName name);
        void RemoveModel(ModelName name);
        void RemoveModel(string path);
    }

    public class StorageService : IStorageService
    {
        [Inject(ContextKeys.CONTEXT_DISPATCHER)]
        public IEventDispatcher dispatcher { get; set; }

        public bool Initialized { get; private set; }

        public void Init()
        {
            if (!Initialized)
            {
                if (!Directory.Exists(PathConstants.LocalDataPath))
                    Directory.CreateDirectory(PathConstants.LocalDataPath);

                Initialized = true;
            }
        }

        public void Dispose()
        {
            Initialized = false;
        }

        public void SaveModel(ModelName name, ISaveModel data, bool useEncryption = false)
        {
            var path = Path.Combine(PathConstants.LocalDataPath, name + PathConstants.BinaryFormat);
            var binaryFormatter = new BinaryFormatter();

            RemoveModel(path);

            var fileStream = File.Open(path, FileMode.Create);
            try
            {
                binaryFormatter.Serialize(fileStream, data);
            }
            catch (Exception e)
            {
                Debug.Log("[StorageService][SaveModel] Serialization exception " + e);
            }
            finally
            {
                fileStream.Dispose();
            }
        }

        public ISaveModel LoadModel(ModelName name)
        {
            var path = Path.Combine(PathConstants.LocalDataPath, name + PathConstants.BinaryFormat);
            var binaryFormatter = new BinaryFormatter();

            if (!File.Exists(path))
            {
                return null;
            }

            var fileStream = File.Open(path, FileMode.Open);
            try
            {
                return (ISaveModel)binaryFormatter.Deserialize(fileStream);
            }
            catch (Exception e)
            {
                Debug.Log("[StorageService][LoadModel] Deserialization exception " + e);
                return null;
            }
            finally
            {
                fileStream.Dispose();
            }
        }

        public T LoadModel<T>(ModelName name)
        {
            return (T)LoadModel(name);
        }

        public void RemoveModel(ModelName name)
        {
            var path = Path.Combine(PathConstants.LocalDataPath, name.ToString() + PathConstants.BinaryFormat);
            RemoveModel(path);
        }

        public void RemoveModel(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
