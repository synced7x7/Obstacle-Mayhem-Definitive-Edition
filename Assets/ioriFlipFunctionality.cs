using System.Data.Common;
using UnityEngine;

public class ioriFlipFunctionality : MonoBehaviour
{
    private Transform ioriTransform;
    private Transform kyoTransform;
    // Update is called once per frame
    private void Awake()
    {
        ioriTransform = GetComponent<Transform>();
    }
    void Update()
    {
        kyoTransform = Kyo_main.Instance.getKyoPosition();
        if(ioriTransform.position.x > kyoTransform.position.x && !Iori_data.Instance.ioriFlipped && !Iori_data.Instance.interval && !Iori_data.Instance.isdodging)
        {
            iori_flip();
            Kyo_physicsHandler.Instance.kyoFlip();
        }
        else if(ioriTransform.position.x < kyoTransform.position.x && Iori_data.Instance.ioriFlipped && !Iori_data.Instance.interval && !Iori_data.Instance.isdodging)
        {
            iori_flip();
            Kyo_physicsHandler.Instance.kyoFlip();
        }
        
    }
    public void iori_flip()
    {
        Vector3 currentScale = ioriTransform.localScale;
        currentScale.x *=-1;
        ioriTransform.localScale = currentScale;
       // Iori_data.Instance.interval = false;
        Iori_data.Instance.ioriFlipped = !Iori_data.Instance.ioriFlipped;
      //  Debug.Log("Iori Flipped: " + Iori_data.Instance.ioriFlipped);
    }
}
