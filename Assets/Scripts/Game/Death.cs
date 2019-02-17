using UnityEngine;
using VRTK;

public class Death : VRTK_ControllerEvents
{
    public bool death = false;
    ForceWarp ForceWarp;
    Replay Replay;

    public int DeathTimes = 1;

    public override void OnGripReleased(ControllerInteractionEventArgs e)
    {

        base.OnGripClicked(e);

        DeathTimes = DeathTimes + 1;

        GameObject gameObject = GameObject.Find("Player");
        ForceWarp = gameObject.GetComponent<ForceWarp>();
        ForceWarp.Warp();

        gameObject = GameObject.Find("Replay");
        Replay = gameObject.GetComponent<Replay>();
        Replay.Play();

        gameObject = GameObject.Find("DeathMeter");
        DeathMeter deathMeter = gameObject.GetComponent<DeathMeter>();
        deathMeter.Dead(DeathTimes);

    }

}
