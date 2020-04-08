using System;
using System.Numerics;
using System.Collections.Generic;
using HiddenObject.Api.Dto;
using HiddenObject.Api.Requests;
using HiddenObject.Api.Responses;
using strange.extensions.context.api;
using strange.extensions.dispatcher.eventdispatcher.api;
using UniRx;
using HiddenObject.Constants;

namespace HiddenObject.Services
{
    public interface INetConnection : IService, IDisposable
    {

        /************************************************************
        *   
        *   Levels
        *   
        ************************************************************/

        IObservable<LevelsResponse> GetLevels();


        /************************************************************
        *   
        *   Progress
        *   
        ************************************************************/

        IObservable<UserProgressResponse> GetUserProgress();

        IObservable<BoolResponse> SetProgress(int level, LevelProgressRequest contract);

        IObservable<BoolResponse> CollectObject(int level, int objectId);

    }

    public class NetConnection : INetConnection
    {
        [Inject(ContextKeys.CONTEXT_DISPATCHER)]
        public IEventDispatcher dispatcher { get; set; }

        public bool Initialized { get; private set; }

        public void Init()
        {
            Initialized = true;
        }

        public void Dispose()
        {
            Initialized = false;
        }

        /************************************************************
        *   
        *   Levels
        *   
        ************************************************************/

        public IObservable<LevelsResponse> GetLevels()
        {
            // tmp fake data
            return Observable
                .Return(new LevelsResponse
                {
                    Levels = new List<LevelDto>
                    {
                        new LevelDto
                        {
                            Level = GameConstants.StartLevel,
                            Name = "Ambulence Level",
                            Description = "Ambulence Level Description",
                            TotalSeconds = 30,
                            SpriteName = "art_ambulance",
                            Objects = new List<ObjectDto>
                            {
                                new ObjectDto
                                {
                                    Id = 1,
                                    Name = "Object1",
                                    SpriteName = "im_object_1",
                                    LocalX = -2.4f,
                                    LocalY = -164.71f,
                                    LocalScale = 1,
                                    Rotation = 0
                                },
                                new ObjectDto
                                {
                                    Id = 2,
                                    Name = "Object2",
                                    SpriteName = "im_object_2",
                                    LocalX = 376f,
                                    LocalY = 212f,
                                    LocalScale = 1,
                                    Rotation = 30.051f
                                },
                                new ObjectDto
                                {
                                    Id = 3,
                                    Name = "Object3",
                                    SpriteName = "im_object_3",
                                    LocalX = -399f,
                                    LocalY = -335f,
                                    LocalScale = 1.77f,
                                    Rotation = -10f
                                }
                            }


                        }
                    }
                })
                .Delay(TimeSpan.FromSeconds(.1f))
                .Single();
        }

        /************************************************************
        *   
        *   Progress
        *   
        ************************************************************/

        public IObservable<UserProgressResponse> GetUserProgress()
        {
            // tmp fake data
            return Observable
                .Return(new UserProgressResponse
                {
                    Progress = new List<LevelProgressDto>()
                })
                .Delay(TimeSpan.FromSeconds(.1f))
                .Single();
        }

        public IObservable<BoolResponse> SetProgress(int level, LevelProgressRequest contract)
        {
            // tmp fake data
            return Observable
                .Return(new BoolResponse
                {
                    Result = true
                }).Single();
        }

        public IObservable<BoolResponse> CollectObject(int level, int objectId)
        {
            // tmp fake data
            return Observable
                .Return(new BoolResponse
                {
                    Result = true
                }).Single();
        }
    }
}