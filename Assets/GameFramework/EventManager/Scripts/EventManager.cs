/*
 *  Gio- 
 *
 *  Known Issue: nothing here, for now
 */

/*#*/

using System.Collections.Generic;
using System;

public class EventManager {

    /// <summary>
    /// Delegate for every function and argument you decide to 
    /// pass. Add new generics if you need 3 or 4 arguments.
    /// </summary>
    public delegate void Callback();
    public delegate void Callback<T>(T argument1);
    public delegate void Callback<T, U>(T argument1, U argument2);

    /// Dictionary holding every event.
    private static Dictionary<string, Delegate> events = new Dictionary<string, Delegate> ();

    /// Generate Event Manager Helper in order to clear dictionary
    /// any time we decide to.
    static private EventManagerHelper eventManagerHelper = (new UnityEngine.GameObject("EventManagerHelper")).AddComponent<EventManagerHelper>();

    /// <summary>
    /// Function that trigger every function associated
    /// to a registered event by its name.
    /// </summary>
    /// <param name="eventName">Event's name</param>
    public static void TriggerEvent(string eventName)
    {
        if (events.ContainsKey(eventName))
        {
            Callback callback = events[eventName] as Callback;

            if(callback != null)
            {
                callback();
            }
        }
    }

    /// <summary>
    /// Function that trigger every function associated
    /// to a registered event by its name with an argument.
    /// </summary>
    /// <typeparam name="T">Any Types</typeparam>
    /// <param name="eventName">Event's name</param>
    /// <param name="arg1">Argument to pass</param>
    public static void TriggerEvent<T>(string eventName, T arg1)
    {
        if (events.ContainsKey(eventName))
        {
            Callback<T> callback = events[eventName] as Callback<T>;

            if (callback != null)
            {
                callback(arg1);
            }
        }
    }

    /// <summary>
    /// Function that trigger every function associated
    /// to a registered event by its name with two arguments.
    /// </summary>
    /// <typeparam name="T">Any Types</typeparam>
    /// <typeparam name="U">Any Types</typeparam>
    /// <param name="eventName">Event's name</param>
    /// <param name="arg1">First Argument to pass</param>
    /// <param name="arg2">Second Argument to pass</param>
    public static void TriggerEvent<T, U>(string eventName, T arg1, U arg2)
    {
        if (events.ContainsKey(eventName))
        {
            Callback<T, U> callback = events[eventName] as Callback<T, U>;

            if (callback != null)
            {
                callback(arg1, arg2);
            }
        }
    }

    /// <summary>
    /// Add a listener to specific event. If event is not 
    /// registrated, then add it in the dictionary.
    /// </summary>
    /// <param name="eventName">Event's name</param>
    /// <param name="listener">Function to execute when event is triggered</param>
    public static void StartListening(string eventName, Callback listener)
    {
        if (!events.ContainsKey(eventName)) events.Add(eventName, null);
        events[eventName] = (Callback)events[eventName] + listener;
    }

    /// <summary>
    /// Add a listener to specific event. If event is not 
    /// registrated, then add it in the dictionary.
    /// </summary>
    /// <typeparam name="T">Any Types</typeparam>
    /// <param name="eventName">Event's name</param>
    /// <param name="listener">Function to execute when event is triggered</param>
    public static void StartListening<T>(string eventName, Callback<T> listener)
    {
        if (!events.ContainsKey(eventName)) events.Add(eventName, null);
        events[eventName] = (Callback<T>)events[eventName] + listener;
    }

    /// <summary>
    /// Add a listener to specific event. If event is not 
    /// registrated, then add it in the dictionary.
    /// </summary>
    /// <typeparam name="T">Any Types</typeparam>
    /// <typeparam name="U">Any Types</typeparam>
    /// <param name="eventName">Event's name</param>
    /// <param name="listener">Function to execute when event is triggered</param>
    public static void StartListening<T, U>(string eventName, Callback<T, U> listener)
    {
        if (!events.ContainsKey(eventName)) events.Add(eventName, null);
        events[eventName] = (Callback<T, U>)events[eventName] + listener;
    }

    /// <summary>
    /// Delete a listener from a registered event. If event
    /// has no more listener, then will be removed.
    /// </summary>
    /// <param name="eventName">Event's name</param>
    /// <param name="handler">Function registered when listener was added</param>
    public static void StopListening(string eventName, Callback handler)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName] = (Callback)events[eventName] - handler;

            if (events[eventName] == null) events.Remove(eventName);
        }
    }

    /// <summary>
    /// Delete a listener from a registered event. If event
    /// has no more listener, then will be removed.
    /// </summary>
    /// <typeparam name="T">Any Types</typeparam>
    /// <param name="eventName">Event's name</param>
    /// <param name="handler">Function registered when listener was added</param>
    public static void StopListening<T>(string eventName, Callback<T> handler)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName] = (Callback<T>)events[eventName] - handler;

            if (events[eventName] == null) events.Remove(eventName);
        }
    }

    /// <summary>
    /// Delete a listener from a registered event. If event
    /// has no more listener, then will be removed.
    /// </summary>
    /// <typeparam name="T">Any Types</typeparam>
    /// <typeparam name="U">Any Types</typeparam>
    /// <param name="eventName">Event's name</param>
    /// <param name="handler">Function registered when listener was added</param>
    public static void StopListening<T, U>(string eventName, Callback<T, U> handler)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName] = (Callback<T, U>)events[eventName] - handler;

            if (events[eventName] == null) events.Remove(eventName);
        }
    }

    /// <summary>
    /// Function to Clean events dictionary.
    /// </summary>
    public static void Clean()
    {
        events.Clear();

        UnityEngine.Debug.LogWarning("Pulizia degli eventi eseguita!");
    }
}
