using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int health = 100;
    public int sanity = 100;
    float Dutch;
    float OrthoSize;
    GameManager gameManager;
    GameObject MainCameraBrain;
    GameObject MainCamera;
    GameObject PostProcess;
    public AudioClip Clock;
    public List<AudioClip> HeartAudioClips;
    public List<AudioClip> BreathingAudioClips;
    AudioSource AudioSource;
    [HideInInspector]
    public bool shake;
    ChromaticAberration ChroAb;
    Vignette vignette;
    ColorAdjustments colorAdjustments;
    PlayerMovement playerMovement;
    public GameObject DeadSprite;
    public int sanityIndex;
    bool stage1Started = false;
    bool stage2Started = false;
    bool stage3Started = false;
    bool stage4Started = false;
    bool isClockOn = false;
    bool IsPlayingClockSound = false;
    bool isHeartOn = false;
    bool IsPlayingHeartSound = false;
    bool canDie = true;
    float R1;
    float R2;
    public int currentHealth;
    bool blink;
    public int currentSanity;
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        AudioSource.Stop();
        currentHealth = health;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        MainCameraBrain = gameManager.MainCameraBrain;
        PostProcess = gameManager.GlobalVolume;
        MainCamera = gameManager.MainCamera;
        SanityChecker();
        PostProcess.GetComponent<Volume>().sharedProfile.TryGet<ChromaticAberration>(out ChroAb);
        PostProcess.GetComponent<Volume>().sharedProfile.TryGet<Vignette>(out vignette);
        PostProcess.GetComponent<Volume>().sharedProfile.TryGet<ColorAdjustments>(out colorAdjustments);
        playerMovement = GetComponent<PlayerMovement>();
        Dutch = MainCameraBrain.GetComponent<CinemachineVirtualCamera>().m_Lens.Dutch;
        OrthoSize = MainCameraBrain.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize;
        MainCameraBrain.GetComponent<CinemachineVirtualCamera>().m_Lens.Dutch = 0;
        MainCameraBrain.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 15;
        ChroAb.active = false;
        vignette.intensity.value = 0.3f;
        Color baseColor = new Color(1, 1, 1);
        colorAdjustments.colorFilter.value = baseColor;
        IsPlayingClockSound = false;
        IsPlayingHeartSound = false;
        isHeartOn = false;
        isClockOn = false;
    }
	private void Update()
	{
        try
		{
            if (currentSanity > 80) { sanityIndex = 0; }
            else if (currentSanity <= 80 && currentSanity >= 60) { sanityIndex = 1; }
            else if (currentSanity < 60 && currentSanity >= 40) { sanityIndex = 2; }
            else if (currentSanity < 40 && currentSanity >= 20) { sanityIndex = 3; }
            else if (currentSanity < 20 && currentSanity > 0) { sanityIndex = 4; }
            SanityChecker();
            switch (gameManager.NumberOfDeath)
            {
                case 0:
                    sanityIndex = 0;
                    break;
                case 1:
                    sanityIndex = 1;
                    break;
                case 2:
                    sanityIndex = 2;
                    break;
                case 3:
                    sanityIndex = 3;
                    break;
                case 4:
                    sanityIndex = 4;
                    break;
            }
            if (shake) 
		    {
                MainCameraBrain.GetComponent<CinemachineVirtualCamera>().m_Lens.Dutch = Mathf.Lerp(Dutch, R1, Random.Range(0.5f, 0.8f) * Time.deltaTime);
                MainCameraBrain.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = Mathf.Lerp(OrthoSize, R2, Random.Range(0.5f, 0.8f) * Time.deltaTime);
                ChroAb.active = true;
                vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0.5f, Random.Range(0.5f, 0.8f) * Time.deltaTime);
            }
            else
		    {
                MainCameraBrain.GetComponent<CinemachineVirtualCamera>().m_Lens.Dutch = Mathf.Lerp(Dutch, 0, 2 * Time.deltaTime);
                MainCameraBrain.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = Mathf.Lerp(OrthoSize, 15, 2 * Time.deltaTime);
                ChroAb.active = false;
                vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0.3f, Random.Range(0.5f, 0.8f) * Time.deltaTime);
            }
            if(blink) { colorAdjustments.postExposure.value = Mathf.Lerp(colorAdjustments.postExposure.value, -10f, 0.005f); }
		    else { colorAdjustments.postExposure.value = Mathf.Lerp(colorAdjustments.postExposure.value, 0, 0.05f); }
        } catch
		{
            return;
		}
	}
	void SanityChecker()
	{
        try
		{
            switch (sanityIndex)
		    {
                case 0:
                    stage1Started = false;
                    stage2Started = false;
                    stage3Started = false;
                    stage4Started = false;
                    IsPlayingClockSound = false;
                    IsPlayingHeartSound = false;
                    isHeartOn = false;
                    isClockOn = false;
                    break;
                case 1:
                    if (!stage1Started) 
                    {
                        R1 = Random.Range(-2f, 2f);
                        R2 = Random.Range(12f, 15f);
                        StartCoroutine(SanityStage1());
                    
                    }
                    break;
                case 2:
                    if (!stage2Started) 
                    {
                        StartCoroutine(SanityStage2()); 
                        R1 = Random.Range(-2f, 2f);
                        R2 = Random.Range(12f, 16f);
                    }
                    break;
                case 3:
                    if (!stage3Started)
                    {
                        StartCoroutine(SanityStage3());
                        R1 = Random.Range(-2f, 2f);
                        R2 = Random.Range(12f, 15f);
                    }
                    break;
                case 4:
                    if (!stage4Started)
                    {
                        StartCoroutine(SanityStage4());
                        R1 = Random.Range(-2f, 2f);
                        R2 = Random.Range(12f, 15f);
                    }
                    break;
                default:
                    break;
            }
		    if (currentSanity <= 0)
		    {
                Die();
		    }
		}
        catch
		{
            return;
		}
	}
    public void applyDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }      
    }
    public void Die()
	{
        if(canDie)
		{
            canDie = false; 
            if(sanityIndex != 4)
		    {
                currentHealth = 0;
                Instantiate(DeadSprite, new Vector3(transform.position.x, transform.position.y - 0.6f, 0), transform.rotation);
                gameManager.NumberOfDeath++;
                gameManager.Restart();
                Destroy(gameObject);
		    }
            else
		    {
                PlayerPrefs.DeleteKey("maxLevelReached");
                gameManager.DefinitiveDeath();
                Destroy(gameObject);
            }
		}
	}
    IEnumerator ClockCoroutine()
	{
        while (IsPlayingClockSound)
		{
            isClockOn = true;
            AudioSource.PlayOneShot(Clock);
            yield return new WaitForSeconds(1);
            yield return new WaitForSeconds(1);
        }
        isClockOn = false;
        yield return null;
    }
    IEnumerator HeartCoroutine()
    {
        while (IsPlayingHeartSound)
        {
            isHeartOn = true;
            int tmp = Random.Range(0, 3);
            switch (tmp)
            {
                case 0:
                    AudioSource.PlayOneShot(HeartAudioClips[tmp]);
                    break;
                case 1:
                    AudioSource.PlayOneShot(HeartAudioClips[tmp]);
                    break;
                case 2:
                    AudioSource.PlayOneShot(HeartAudioClips[tmp]);
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(1f);
        }
        isHeartOn = false;
        yield return null;
    }
    IEnumerator SanityStage1()
	{
        if (sanityIndex == 1) {
            IsPlayingClockSound = false;
            IsPlayingHeartSound = false;
            if (!stage1Started)
            {
                //random screenshakes
                stage1Started = true;
                StartCoroutine(Shake(Random.Range(1f, 2.5f)));
                yield return new WaitForSeconds(Random.Range(5f, 10f));
                stage1Started = false;
            }
        }
        yield return null;

	}
    IEnumerator SanityStage2()
    {
        if (sanityIndex == 2)
        {
            IsPlayingClockSound = true;
            IsPlayingHeartSound = false;
            if (!isClockOn)
			{
                StartCoroutine(ClockCoroutine());
            }
            if (!stage2Started)
            {
                stage2Started = true;
                int temp = Random.Range(1, 3);
                switch (temp)
                {
                    case 1:
                        //random screenshakes
                        StartCoroutine(Shake(Random.Range(1f, 2.5f)));
                        break;
                    case 2:
                        //random stops
                        StartCoroutine(MovementStop(Random.Range(1f, 1.5f)));
                        break;
                    default:
                        break;
                }
                yield return new WaitForSeconds(Random.Range(5f, 10f));
                stage2Started = false;
            }
        }
        yield return null;
    }
    IEnumerator SanityStage3()
    {
        if (sanityIndex == 3)
        {
            if (!stage3Started)
            {
                IsPlayingClockSound = true;
                IsPlayingHeartSound = false;
                if (!isClockOn)
                {
                    StartCoroutine(ClockCoroutine());
                }
                stage3Started = true;
                //stops all clock sounds    
                //random screenshakes
                //random stops to retake his breath
                //random moves
                //clock sounds intensifies
                int temp = Random.Range(1, 4);
                switch (temp)
                {
                    case 1:
                        //random screenshakes
                        StartCoroutine(Shake(Random.Range(1f, 2.5f)));
                        break;
                    case 2:
                        //random stops
                        StartCoroutine(MovementStop(Random.Range(1f, 1.5f)));
                        break;
                    case 3:
                        StartCoroutine(MovementRandom());
                        break;
                    default:
                        break;
                }
                yield return new WaitForSeconds(Random.Range(5f, 10f));
                stage3Started = false;
            }
        }
        yield return null;
    }
    IEnumerator SanityStage4()
    {
        if (sanityIndex == 4)
        {
            if (!stage4Started)
            {
                stage4Started = true;
                IsPlayingClockSound = true;
                IsPlayingHeartSound = true;
                if (!isHeartOn)
                {
                    StartCoroutine(HeartCoroutine());
                }
                if (!isClockOn)
                {
                    StartCoroutine(ClockCoroutine());
                }
                //stops all clock and heart sounds
                //random stops to retake his breath more frequent
                //clock sounds intensifies
                //heart pounding sound
                
                int temp = Random.Range(1, 6);
                switch (temp)
                {
                    case 1:
                        //random screenshakes
                        StartCoroutine(Shake(Random.Range(1f, 2.5f)));
                        break;
                    case 2:
                        //random stops
                        StartCoroutine(MovementStop(Random.Range(2.5f, 3f)));
                        break;
                    case 3:
                        //times fasten an slows randomly
                        StartCoroutine(RandomTimeScale(Random.Range(2f, 2.5f)));
                        break;
                    case 4:
                        //random moves
                        StartCoroutine(MovementRandom());
                        break;
                    case 5:
                        //screen is sometimes turning red
                        StartCoroutine(TurnScreenRedBlinks());
                        break;
                    default:
                        break;
                }
                yield return new WaitForSeconds(Random.Range(6f, 6f));
                stage4Started = false;
            }
        }
        yield return null;
    }
    IEnumerator Shake(float Duration)
	{
        shake = true;
        yield return new WaitForSeconds(Duration);
        shake = false;
        yield return null;
    }
    IEnumerator MovementStop(float Duration)
	{
        if(playerMovement != null ) 
        { 
            playerMovement.CanMove = false;
            int tmp = Random.Range(0, 2);
            switch (tmp)
            {
                case 0:
                    AudioSource.PlayOneShot(BreathingAudioClips[tmp]);
                    break;
                case 1:
                    AudioSource.PlayOneShot(BreathingAudioClips[tmp]);
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(Duration);
            playerMovement.CanMove = true;
        }
        yield return null;
	}
    IEnumerator RandomTimeScale(float Duration)
	{
        Time.timeScale = Random.Range(0.5f, 1.5f);
        yield return new WaitForSeconds(Duration);
        Time.timeScale = 1;
        yield return null;
    }
    IEnumerator MovementRandom()
    {
        playerMovement.IsControled = true;
        playerMovement.InputX = Random.Range(-0.5f, 0.5f);
        yield return new WaitForSeconds(Random.Range(0.1f, 0.7f));
        playerMovement.InputX = Random.Range(-0.5f, 0.5f);
        yield return new WaitForSeconds(Random.Range(0.1f, 0.7f));
        playerMovement.InputX = Random.Range(-0.5f, 0.5f);
        playerMovement.IsControled = false;
        yield return null;
    }
    IEnumerator TurnScreenRedBlinks()
	{
        Color colorRed = new Color(1, 0.45f, 0.45f);
        Color baseColor = new Color(1, 1, 1);
        blink = true;
        yield return new WaitForSeconds(0.5f);
        colorAdjustments.colorFilter.value = colorRed;
        yield return new WaitForSeconds(0.5f);
        blink = false;

        yield return new WaitForSeconds(2f);

        blink = true;
        yield return new WaitForSeconds(0.5f);
        colorAdjustments.colorFilter.value = baseColor;
        yield return new WaitForSeconds(0.5f);
        blink = false;
    }
}
