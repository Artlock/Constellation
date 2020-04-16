using UnityEngine;

public class SpaceCreator : MonoBehaviour
{
    public Star starPrefab;
    public Constellation constellationPrefab;
    public GameObject groupPrefab;
    public LineRenderer linePrefab;

    public AudioClip soundStar;

    //We Create The Stars - GUMIHO
    public void CreateTheStar(Vector2 position, Constellation constellation)
    {
        Star star = Instantiate(starPrefab, constellation.transform);
        star.Initialize(constellation.colorOfConstellation, position);

        constellation.LinkedStars(star);

        GameManager.Instance.line.SetPosition(0, position);

        ParticleSystem particle = GameManager.Instance.ParticleFlux();
        ParticleSystem.MainModule mainModule = particle.main;
        mainModule.startColor = constellation.colorOfConstellation;
        particle.transform.position = star.transform.position;
        particle.Play();

        SoundManager.Instance.SoundEffect(soundStar);

        CameraManager.Instance.FollowMe(position);
    }

    //We Create The Stars - GUMIHO
    public Star CreateTheStar(Vector2 position, Color color, Transform groupTransform)
    {
        Star star = Instantiate(starPrefab, groupTransform);
        star.Initialize(color, position, false);

        GameManager.Instance.line.SetPosition(0, position);

        ParticleSystem particle = GameManager.Instance.ParticleFlux();
        ParticleSystem.MainModule mainModule = particle.main;
        mainModule.startColor = color;
        particle.transform.position = star.transform.position;
        particle.Play();

        SoundManager.Instance.SoundEffect(soundStar);

        return star;
    }

    //A New Constellation - Quantic, The Western Transient
    public void NewConstellation()
    {
        Constellation constellation = Instantiate(constellationPrefab);
        constellation.Initialize();

        GameManager.Instance.NewBranche(constellation);
    }
}
