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
    private static Dictionary<EventsID, Delegate> events = new Dictionary<EventsID, Delegate> ();

    /// Generate Event Manager Helper in order to clear dictionary
    /// any time we decide to.
    static private EventManagerHelper eventManagerHelper = (new UnityEngine.GameObject("EventManagerHelper")).AddComponent<EventManagerHelper>();

    /// <summary>
    /// Function that trigger every function associated
    /// to a registered event by its name.
    /// </summary>
    /// <param name="eventID">Event's name</param>
    public static void TriggerEvent(EventsID eventID)
    {
        if (events.ContainsKey(eventID))
        {
            (events[eventID] as Callback)?.Invoke();
        }
    }

    /// <summary>
    /// Function that trigger every function associated
    /// to a registered event by its name with an argument.
    /// </summary>
    /// <typeparam name="T">Any Types</typeparam>
    /// <param name="eventID">Event's name</param>
    /// <param name="arg1">Argument to pass</param>
    public static void TriggerEvent<T>(EventsID eventID, T arg1)
    {
        if (events.ContainsKey(eventID))
        {
            (events[eventID] as Callback<T>)?.Invoke(arg1);
        }
    }

    /// <summary>
    /// Function that trigger every function associated
    /// to a registered event by its name with two arguments.
    /// </summary>
    /// <typeparam name="T">Any Types</typeparam>
    /// <typeparam name="U">Any Types</typeparam>
    /// <param name="eventID">Event's name</param>
    /// <param name="arg1">First Argument to pass</param>
    /// <param name="arg2">Second Argument to pass</param>
    public static void TriggerEvent<T, U>(EventsID eventID, T arg1, U arg2)
    {
        if (events.ContainsKey(eventID))
        {
            (events[eventID] as Callback<T, U>)?.Invoke(arg1, arg2);
        }
    }

    /// <summary>
    /// Add a listener to specific event. If event is not 
    /// registrated, then add it in the dictionary.
    /// </summary>
    /// <param name="eventID">Event's name</param>
    /// <param name="listener">Function to execute when event is triggered</param>
    public static void StartListening(EventsID eventID, Callback listener)
    {
        if (!events.ContainsKey(eventID)) events.Add(eventID, null);
        events[eventID] = (Callback)events[eventID] + listener;
    }

    /// <summary>
    /// Add a listener to specific event. If event is not 
    /// registrated, then add it in the dictionary.
    /// </summary>
    /// <typeparam name="T">Any Types</typeparam>
    /// <param name="eventID">Event's name</param>
    /// <param name="listener">Function to execute when event is triggered</param>
    public static void StartListening<T>(EventsID eventID, Callback<T> listener)
    {
        if (!events.ContainsKey(eventID)) events.Add(eventID, null);
        events[eventID] = (Callback<T>)events[eventID] + listener;
    }

    /// <summary>
    /// Add a listener to specific event. If event is not 
    /// registrated, then add it in the dictionary.
    /// </summary>
    /// <typeparam name="T">Any Types</typeparam>
    /// <typeparam name="U">Any Types</typeparam>
    /// <param name="eventID">Event's name</param>
    /// <param name="listener">Function to execute when event is triggered</param>
    public static void StartListening<T, U>(EventsID eventID, Callback<T, U> listener)
    {
        if (!events.ContainsKey(eventID)) events.Add(eventID, null);
        events[eventID] = (Callback<T, U>)events[eventID] + listener;
    }

    /// <summary>
    /// Delete a listener from a registered event. If event
    /// has no more listener, then will be removed.
    /// </summary>
    /// <param name="eventID">Event's name</param>
    /// <param name="handler">Function registered when listener was added</param>
    public static void StopListening(EventsID eventID, Callback handler)
    {
        if (events.ContainsKey(eventID))
        {
            events[eventID] = (Callback)events[eventID] - handler;

            if (events[eventID] == null) events.Remove(eventID);
        }
    }

    /// <summary>
    /// Delete a listener from a registered event. If event
    /// has no more listener, then will be removed.
    /// </summary>
    /// <typeparam name="T">Any Types</typeparam>
    /// <param name="eventID">Event's name</param>
    /// <param name="handler">Function registered when listener was added</param>
    public static void StopListening<T>(EventsID eventID, Callback<T> handler)
    {
        if (events.ContainsKey(eventID))
        {
            events[eventID] = (Callback<T>)events[eventID] - handler;

            if (events[eventID] == null) events.Remove(eventID);
        }
    }

    /// <summary>
    /// Delete a listener from a registered event. If event
    /// has no more listener, then will be removed.
    /// </summary>
    /// <typeparam name="T">Any Types</typeparam>
    /// <typeparam name="U">Any Types</typeparam>
    /// <param name="eventID">Event's name</param>
    /// <param name="handler">Function registered when listener was added</param>
    public static void StopListening<T, U>(EventsID eventID, Callback<T, U> handler)
    {
        if (events.ContainsKey(eventID))
        {
            events[eventID] = (Callback<T, U>)events[eventID] - handler;

            if (events[eventID] == null) events.Remove(eventID);
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
