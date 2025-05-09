﻿//#define Verbose

using RobustFSM.Interfaces;
using System;
using UnityEngine;

namespace RobustFSM.Base
{
    [Serializable]
    public class BState : IState
    {
        #region Propeties

        /// <summary>
        /// A reference to the name of this instance
        /// </summary>
        public virtual string StateName { get; set; }

        /// <summary>
        /// A reference to the state machine that this instance belongs to
        /// </summary>
        public IFSM Machine { get; set; }

        /// <summary>
        /// A reference to the super machine that all sub statemachines belong to
        /// </summary>
        public IFSM SuperMachine { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrievs the state machine this instance belongs to
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <returns>the state machine</returns>
        public T GetMachine<T>() where T : IFSM
        {
            return (T)Machine;
        }

        /// <summary>
        /// Retrievs the super state machine this instance belongs to
        /// </summary>
        /// <typeparam name="T">return type</typeparam>
        /// <returns>the state machine</returns>
        public T GetSuperMachine<T>() where T : IFSM
        {
            return (T)SuperMachine;
        }

        #endregion

        #region Virtual Methods

        /// <summary>
        /// Is called every time this state is activated
        /// </summary>
        public virtual void Enter()
        {
#if Verbose
            //Console.WriteLine(Machine.MachineName + ":Enter(" + StateName + ")");
            Debug.Log("<color=green>" + Machine.MachineName + ":Enter(" + StateName + ")</color>");
#endif
        }

        /// <summary>
        /// Is called every frame update
        /// </summary>
        public virtual void Execute() { }

        /// <summary>
        /// Is called every time this state is deactivated
        /// </summary>
        public virtual void Exit()
        {
#if Verbose
            Debug.Log("<color=red>" + Machine.MachineName + ":Exit(" + StateName + ")</color>");
#endif
        }

        /// <summary>
        /// Initializes this instance
        /// </summary>
        public virtual void Initialize()
        {
            ///set state name
            if (string.IsNullOrEmpty(StateName))
                StateName = GetType().Name;

#if Verbose
            Debug.Log("<color=blue>" + Machine.MachineName + ":Initialize(" + StateName + ")</color>");
#endif
        }

        /// <summary>
        /// Is called on every manual execute in the state machine
        /// </summary>
        public virtual void ManualExecute() { }

        /// <summary>
        /// Is called on every physics execute in the state machine
        /// </summary>
        public virtual void PhysicsExecute() { }

        public virtual void OnAnimatorExecuteIK(int layerIndex) { }

        public virtual void OnAnimatorExecuteMove() { }

        public virtual void OnControllerColliderCollide(ControllerColliderHit hit) { }

        public virtual void OnCollisionStart<T>(T collision) { }

        #endregion
    }
}