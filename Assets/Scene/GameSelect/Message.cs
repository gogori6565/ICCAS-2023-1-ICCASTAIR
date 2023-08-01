using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public GameObject donggle;
    public GameObject CanvasDonggle;
    public GameObject encourageText;
    private float setTime = 7f;

    public string[] encouragement_messages = {
    "Difficulties are temporary. Hang in there a little longer.",
    "What you're going through is solvable.",
    "I'm truly moved by your dedication during your OCD cognitive-behavioral therapy.",
    "I genuinely appreciate your efforts.",
    "I'll be watching your growth and progress. You'll see a better version of yourself.",
    "No matter how tough it gets, I'll always be by your side.",
    "Take pride in persevering and making progress step by step.",
    "It's inspiring to see how actively you engage in your therapy.",
    "Believe that you can overcome OCD.",
    "You'll become stronger through your battles with OCD.",
    "You're stronger than you think.",
    "I'm here to listen to your story whenever you need.",
    "Everything will be okay. Trust me.",
    "You have the ability to conquer your OCD.",
    "Take pride in enduring through this challenging time.",
    "I believe you can overcome OCD.",
    "You are much stronger than you believe.",
    "Focus on making progress, even if it's just a little each day.",
    "You have the power to improve your skills.",
    "Thank you for your daily efforts. They mean a lot.",
    "Let's overcome this difficult time together.",
    "Feel proud of yourself for enduring this challenging phase.",
    "Your efforts will never go in vain.",
    "I'll support you in becoming even stronger.",
    "You are more resilient than you know.",
    "It's okay to experience all kinds of emotions. It's normal.",
    "I see the effort you're putting into overcoming your challenges.",
    "I acknowledge and recognize what you're going through.",
    "I believe you can overcome your current difficulties.",
    "Your determination in fighting OCD is truly admirable.",
    "You have my full support in what you're enduring.",
    "You'll overcome this difficult time.",
    "You believe in yourself, and that's amazing.",
    "You'll become stronger through what you're experiencing.",
    "Your proactive approach to tackling challenges is impressive.",
    "I'll be by your side, sharing your pain and struggles.",
    "Your resilience in coping with life's challenges is impressive.",
    "Everything you're going through now is part of your growth.",
    "I know how strong you are. There's nothing you can't overcome.",
    "I'm amazed by your efforts in fighting against your OCD.",
    "You're inspiring me with your dedication.",
    "I believe you can make it through. We'll go through this together.",
    "You have the strength to overcome what you're facing.",
    "You're working towards a better life, and that's incredible.",
    "Your tenacity in fighting alongside fear is truly remarkable.",
    "Remember that you love yourself.",
    "You'll find happiness in the future.",
    "Your efforts will never be in vain.",
    "It's amazing to see you working hard to overcome your current challenges.",
    "You're moving towards a better life. Keep going, and I'll be there with you."
    };


    public string[] asymmetry_messages = {
        "It's okay not to make things symmetrical, asymmetry looks cool too.",
        "Asymmetry is beautiful too, feel free to create that way.",
        "You don't need symmetry, asymmetry can be lovely.",
        "It's fine if it's not symmetrical, draw with freedom.",
        "Your artwork can be fantastic even without symmetry, draw as you like.",
        "It's okay if it's not symmetrical, your unique style is great.",
        "Don't worry about symmetry, asymmetry can be wonderful.",
        "Even without symmetry, your artwork is impressive.",
        "Asymmetry is beautiful, express yourself freely.",
        "It doesn't have to be symmetrical, go ahead and draw with creativity.",
        "Even without symmetry, your artwork is amazing, express yourself freely.",
        "It's okay if it's not symmetrical, draw with freedom.",
        "Asymmetry is also lovely, be creative in your own way.",
        "Even without symmetry, your artwork can be beautiful, draw as you like.",
        "You don't need to make it symmetrical, express yourself freely.",
        "Asymmetry is beautiful too, feel free to create that way.",
        "It's okay if it's not symmetrical, your unique style is great.",
        "Don't worry about symmetry, asymmetry can be wonderful.",
        "Even without symmetry, your artwork is impressive.",
        "Asymmetry is beautiful, express yourself freely."
    };


    public string[] checking_messages = {
        "Believe a little and take comfort, it's okay not to check.",
        "It's okay not to check, everything will be fine.",
        "Have faith and take comfort, you don't need to check.",
        "It's okay not to check, just believe.",
        "There won't be any problem without checking, put your mind at ease.",
        "You don't need to worry, it's okay not to check.",
        "You don't need to worry, no need to check.",
        "Relax, it's okay not to check.",
        "It's okay not to check, everything will work out.",
        "Relax your mind, it's okay not to check.",
        "It's okay not to check, no need to worry.",
        "Relax, it's okay not to check.",
        "It's okay not to check, everything will fall into place.",
        "You don't need to worry, it's okay not to check.",
        "Have faith and believe, it's okay not to check.",
        "It's okay not to check, put your mind at ease.",
        "It's okay not to check, everything will be fine.",
        "You don't need to worry, it's okay not to check.",
        "Relax, it's okay not to check.",
        "It's okay not to check, everything will work out.",
        "Relax your mind, it's okay not to check.",
        "It's okay not to check, no need to worry.",
        "Relax, it's okay not to check.",
        "It's okay not to check, everything will fall into place.",
        "You don't need to worry, it's okay not to check.",
        "Relax, it's okay not to check.",
        "It's okay not to check, put your mind at ease.",
        "It's okay not to check, everything will be fine.",
        "Relax, it's okay not to check.",
        "It's okay not to check, everything will work out."
    };

    public string[] handwashing_messages = {
        "Handwashing is good, but you don't need to do it too vigorously.",
        "You don't have to wash your hands too vigorously, just wash them thoroughly.",
        "It's okay to wash your hands gently.",
        "Handwashing is good, you don't have to do it too forcefully.",
        "It's good to wash your hands frequently, but you can do it less forcefully.",
        "Of course, handwashing is good, but you don't have to do it too forcefully.",
        "It's good to wash your hands gently, no need to do it forcefully.",
        "You don't need to wash your hands too vigorously, just wash them thoroughly.",
        "Washing your hands frequently is good, but you don't have to do it too forcefully.",
        "Just wash your hands gently, you don't have to do it too forcefully.",
        "Washing your hands frequently is good, but you don't have to do it too forcefully.",
        "It's good to wash your hands gently, no need to do it forcefully.",
        "Washing your hands frequently is good, but you don't have to do it too forcefully.",
        "Washing your hands will make them clean, no need to do it too forcefully.",
        "Washing your hands frequently is good, just wash them gently.",
        "You don't have to wash your hands too forcefully, just wash them thoroughly.",
        "Washing your hands gently is good, no need to do it too forcefully.",
        "Washing your hands is good, but you don't have to do it too forcefully.",
        "Washing your hands frequently is good, but you don't have to do it too forcefully.",
        "Just wash your hands gently, you don't have to do it too forcefully.",
        "Washing your hands frequently is good, no need to do it too forcefully.",
        "Washing your hands gently is good, just wash them thoroughly.",
        "Washing your hands is good, but you don't have to do it too forcefully.",
        "Washing your hands frequently is good, but you don't have to do it too forcefully.",
        "Just wash your hands gently, you don't have to do it too forcefully.",
        "Washing your hands frequently will make them clean, just wash them gently.",
        "Washing your hands gently is good, no need to do it too forcefully.",
        "Washing your hands is good, but you don't have to do it too forcefully.",
        "Washing your hands frequently is good, but you don't have to do it too forcefully.",
        "Just wash your hands gently, you don't have to do it too forcefully.",
        "Washing your hands frequently will make them clean, just wash them gently."
    };


   private bool flag = false;

    void Start()
    {
        CanvasDonggle.SetActive(false);
        donggle.SetActive(false);
    }

    void Update()
    {
        setTime -= Time.deltaTime;
        
        if (flag == false)
        {
            if (setTime < 0)
            {
                int randomIndex = Random.Range(0, encouragement_messages.Length);
                encourageText.GetComponent<Text>().text = encouragement_messages[randomIndex];
                donggle.SetActive(true);
                donggle.GetComponent<Animator>().speed = 1f;
                CanvasDonggle.SetActive(true);
                flag = true;
            }
        }
        else 
        { 
            if(setTime < -5)
            {
                CanvasDonggle.SetActive(false);
                donggle.SetActive(false);
                setTime = 7f;
                flag = false;
            }
        }
    }
}
