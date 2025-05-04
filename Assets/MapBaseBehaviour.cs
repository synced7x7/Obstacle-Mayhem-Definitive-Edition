using UnityEngine;

public class MapBaseBehaviour : MonoBehaviour
{
    public static MapBaseBehaviour Instance { get; private set; }
    public enum map
    {
        eclipseOfSerenity,
        eclipseOfDespair,
        trueEclipseOfSerenity,
        trueEclipseOfDespair
    }
    public bool mapFlag;
    public bool intermediateMapFlag;
    public bool eclipseOfSerenity;
    public bool eclipseOfDespair;
    public bool trueEclipseOfDespair;
    public bool trueEclipseOfSerenity;
    [SerializeField] coloredFlashMap EclipseOfSerenityColoredFlash;
    [SerializeField] coloredFlashMap EclipseOfDespairColoredFlash;
    [SerializeField] coloredFlashMap TrueEclipseOfSerenityColoredFlash;
    [SerializeField] coloredFlashMap TrueEclipseOfDespairColoredFlash;



    private void Awake()
    {
        #region instance
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        #endregion
        eclipseOfSerenity = true;
    }

    public void mapActivator(map mpp)
    {
        if (!mapFlag)
        {

            switch (mpp)
            {
                case map.eclipseOfDespair:
                    if (!intermediateMapFlag)
                    {
                        resetMap();
                        eclipseofDespairBehaviour.Instance.eclipseOfDespairMapActivator();
                        rainBehaviour.Instance.rainActivator();
                        eclipseOfDespair = true;
                        intermediateMapFlag = true;
                        Debug.Log("Eclipse of Despair Activated");
                    }
                    break;
                case map.trueEclipseOfSerenity:
                    if (!intermediateMapFlag)
                    {
                        resetMap();
                        trueEclipseofSerenityBehaviour.Instance.trueEclipseOfSrenityMapActivator();
                        trueEclipseOfSerenity = true;
                        mapFlag = true;
                        Debug.Log("True Eclipse of Serenity Activated");
                    }
                    break;
                case map.trueEclipseOfDespair:
                    resetMap();
                    trueEclipseofDespairBehaviour.Instance.trueEclipseOfDespairMapActivator();
                    rainBehaviour.Instance.rainActivator();
                    trueEclipseOfDespair = true;
                    mapFlag = true;
                    Debug.Log("True Eclipse of Despair Activated");
                    break;
                case map.eclipseOfSerenity:
                    resetMap();
                    eclipseOfSerenityBehaviour.Instance.eclipseOfSrenityMapActivator();
                    trueEclipseOfSerenity = true;
                    Debug.Log("Eclipse of Serenity Activated");
                    break;
                default:
                    Debug.Log("invalid map enum");
                    break;
            }
        }
    }
    private void resetMap()
    {
        eclipseOfSerenityBehaviour.Instance.eclipseOfSrenityMapDeactivator();
        eclipseofDespairBehaviour.Instance.eclipseOfDespairMapDeactivator();
        trueEclipseofSerenityBehaviour.Instance.trueEclipseOfSrenityMapDeactivator();
        trueEclipseofDespairBehaviour.Instance.trueEclipseOfDespairMapDeactivator();
        eclipseOfSerenity = false;
        eclipseOfDespair = false;
        trueEclipseOfDespair = false;
        trueEclipseOfSerenity = false;
    }
    public void MAPFadeINFadeOutBasedOnTime(float timeBetweenFade, float fadeDuration, Color targetColor)
    {
        if (eclipseOfSerenity)
        {
            eclipseOfSerenityBehaviour.Instance.EclipseOfSerenityFade(timeBetweenFade, fadeDuration, targetColor);
        }
        else if (eclipseOfDespair)
        {
            eclipseofDespairBehaviour.Instance.EclipseOfDespairFade(timeBetweenFade, fadeDuration, targetColor);
        }
        else if (trueEclipseOfDespair)
        {
            trueEclipseofDespairBehaviour.Instance.trueEclipseOfDespairFade(timeBetweenFade, fadeDuration, targetColor);
        }
        else
        {
            trueEclipseofSerenityBehaviour.Instance.trueEclipseOfSerenityFade(timeBetweenFade, fadeDuration, targetColor);
        }
    }
    public void MapFadeIN(float fadeDuration, Color targetColor)
    {
        if (eclipseOfSerenity)
        {
            eclipseOfSerenityBehaviour.Instance.FadeIN(fadeDuration, targetColor);
        }
        else if (eclipseOfDespair)
        {
            eclipseofDespairBehaviour.Instance.FadeIN(fadeDuration, targetColor);
        }
        else if (trueEclipseOfDespair)
        {
            trueEclipseofDespairBehaviour.Instance.FadeIN(fadeDuration, targetColor);
        }
        else
        {
            trueEclipseofSerenityBehaviour.Instance.FadeIN(fadeDuration, targetColor);
        }
    }
    public void MapFadeOut(float fadeDuration)
    {
        if (eclipseOfSerenity)
        {
            eclipseOfSerenityBehaviour.Instance.FadeOut(fadeDuration);
        }
        else if (eclipseOfDespair)
        {
            eclipseofDespairBehaviour.Instance.FadeOut(fadeDuration);
        }
        else if (trueEclipseOfDespair)
        {
            trueEclipseofDespairBehaviour.Instance.FadeOut(fadeDuration);
        }
        else
        {
            trueEclipseofSerenityBehaviour.Instance.FadeOut(fadeDuration);
        }
    }

    public void customMapActivator() // disabled and activated for the special moves transition
    {
        if (eclipseOfSerenity)
        {
            eclipseOfSerenityBehaviour.Instance.eclipseOfSrenityMapActivator();
        }
        else if (eclipseOfDespair)
        {
            eclipseofDespairBehaviour.Instance.eclipseOfDespairMapActivator();
        }
        else if (trueEclipseOfDespair)
        {
            trueEclipseofDespairBehaviour.Instance.trueEclipseOfDespairMapActivator();
        }
        else if (trueEclipseOfSerenity)
        {
            trueEclipseofSerenityBehaviour.Instance.trueEclipseOfSrenityMapActivator();
        }
    }
    public void customMapDeactivator()
    {
        if (eclipseOfSerenity)
        {
            eclipseOfSerenityBehaviour.Instance.eclipseOfSrenityMapDeactivator();
        }
        else if (eclipseOfDespair)
        {
            eclipseofDespairBehaviour.Instance.eclipseOfDespairMapDeactivator();
        }
        else if (trueEclipseOfDespair)
        {
            trueEclipseofDespairBehaviour.Instance.trueEclipseOfDespairMapDeactivator();
        }
        else if (trueEclipseOfSerenity)
        {
            trueEclipseofSerenityBehaviour.Instance.trueEclipseOfSrenityMapDeactivator();
        }
    }
    public void ColoredFlashMAPAlphaActivator(Color color, float duration)
    {
        if (eclipseOfSerenity)
        {
            EclipseOfSerenityColoredFlash.flashAlpha(color, duration);
        }
        else if (eclipseOfDespair)
        {
            EclipseOfDespairColoredFlash.flashAlpha(color, duration);
        }
        else if (trueEclipseOfDespair)
        {
            TrueEclipseOfDespairColoredFlash.flashAlpha(color, duration);
        }
        else if (trueEclipseOfSerenity)
        {
            TrueEclipseOfSerenityColoredFlash.flashAlpha(color, duration);
        }
    }
    public void ColoredFlashMAPActivator(Color color, float duration)
    {
        if (eclipseOfSerenity)
        {
            EclipseOfSerenityColoredFlash.Flash(color, duration);
        }
        else if (eclipseOfDespair)
        {
            EclipseOfDespairColoredFlash.Flash(color, duration);
        }
        else if (trueEclipseOfDespair)
        {
            TrueEclipseOfDespairColoredFlash.Flash(color, duration);
        }
        else if (trueEclipseOfSerenity)
        {
            TrueEclipseOfSerenityColoredFlash.Flash(color, duration);
        }
    }
    public void resetColor()
    {
        if (eclipseOfSerenity)
        {
            EclipseOfSerenityColoredFlash.resetColor();
        }
        else if (eclipseOfDespair)
        {
            EclipseOfDespairColoredFlash.resetColor();
        }
        else if (trueEclipseOfDespair)
        {
            TrueEclipseOfDespairColoredFlash.resetColor();
        }
        else if (trueEclipseOfSerenity)
        {
            TrueEclipseOfSerenityColoredFlash.resetColor();
        }
    }

}
