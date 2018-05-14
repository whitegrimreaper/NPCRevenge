using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class TrapController : MonoBehaviour {
    protected Trap trapStats;
    protected static System.Timers.Timer resetTimer = new System.Timers.Timer();
    protected static bool needsToBeReset = false;

    // Specify what you want to happen when the Elapsed event is raised.
    private static void OnTimedEvent(object source, ElapsedEventArgs e) {
        needsToBeReset = true;
        resetTimer.Enabled = false;
    }

    // Update is called at initialization
    void Start() {
        resetTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
    }

    void reset() {
        needsToBeReset = true;
    }

    void setTrapStats(string trapName) {
        trapStats = GameObject.Find("TrapDataBase").GetComponent<TrapDataBase>().trapDataBase.Find((x => x.Name.Contains(trapName)));
    }
}
