using VRTK;

public class HandleButtonDoor : VRTK_InteractableObject
{

    public bool open = false;


    public override void StartUsing(VRTK_InteractUse usingObject)
    {
        base.StartUsing(usingObject);
        open = true;
    }

    public override void StopUsing(VRTK_InteractUse usingObject)
    {
        base.StopUsing(usingObject);
        open = false;
    }

}
