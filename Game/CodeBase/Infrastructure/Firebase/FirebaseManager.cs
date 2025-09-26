using Firebase;
using Firebase.Auth;
using UnityEngine;
using VContainer.Unity;

namespace Infrastructure.Firebase
{
    public sealed class FirebaseManager : IInitializable
    {
        private FirebaseApp _app;

        public void Initialize()
        {
            InitializeFirebase();
        }

        void InitializeFirebase()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    _app = FirebaseApp.DefaultInstance;
                    Debug.Log("Firebase initialized successfully");
                
                    InitializeAuth();
                }
                else
                {
                    Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                }
            });
        }

        void InitializeAuth()
        {
            FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        }
    }
}