using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class PostProcessing : MonoBehaviour
{
    public ColorParameter colorFilter;
    public PostProcessVolume volume;
    //private Bloom bloom;
    //public override void Interp(Color , Color , float)
    // Start is called before the first frame update
    void Start()
    {
        volume.weight = 0;
        //bloom = volume.profile.AddSettings<Bloom>();
        /*Color x = (0,0,0,1)
        _color.colorFilter = (Color x, Color y, 5.0f);*/
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (GameManager.Instance.screenFilter == true && volume.weight < 1)
        { volume.weight += 0.1f; }
        else if (GameManager.Instance.screenFilter == false && volume.weight > 0)
        { volume.weight -= 0.1f; }
        //bloom.intensity.value = 10 * Time.time % 2;
    }
}
