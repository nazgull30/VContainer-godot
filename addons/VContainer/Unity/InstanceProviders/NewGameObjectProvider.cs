// using System;
// using System.Collections.Generic;
// using Godot;
//
// namespace VContainer.Unity
// {
//     internal readonly struct NewGameObjectProvider : IInstanceProvider
//     {
//         readonly Type componentType;
//         readonly IInjector injector;
//         readonly IReadOnlyList<IInjectParameter> customParameters;
//         readonly string newGameObjectName;
//         readonly ComponentDestination? destination;
//
//         public NewGameObjectProvider(
//             Type componentType,
//             IInjector injector,
//             IReadOnlyList<IInjectParameter> customParameters,
//             in ComponentDestination? destination,
//             string newGameObjectName = null)
//         {
//             this.componentType = componentType;
//             this.customParameters = customParameters;
//             this.injector = injector;
//             this.destination = destination;
//             this.newGameObjectName = newGameObjectName;
//         }
//
//         public object SpawnInstance(IObjectResolver resolver)
//         {
//             var name = string.IsNullOrEmpty(newGameObjectName)
//                 ? componentType.Name
//                 : newGameObjectName;
//             var gameObject = new Node2D() {Name = name};
//             gameObject.Visible = false;
//
//             var parent = destination?.GetParent();
//             if (parent != null)
//             {
//                 gameObject.GetParent().RemoveChild(gameObject);
//                 parent.AddChild(gameObject);
//             }
//             var component = gameObject.AddComponent(componentType);
//
//             injector.Inject(component, resolver, customParameters);
//             component.gameObject.SetActive(true);
//             return component;
//         }
//     }
// }