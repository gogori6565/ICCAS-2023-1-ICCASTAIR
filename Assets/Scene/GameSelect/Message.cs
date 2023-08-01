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
    "You're moving towards a better life. Keep going, and I'll be there with you.",
    "You are not alone in this journey; we will face it together.",
    "I'm proud of your commitment to healing and growth.",
    "You have the courage to challenge your fears and thoughts.",
    "Your progress is a testament to your strength and determination.",
    "You are taking steps towards a healthier and happier life.",
    "I believe in your ability to overcome the challenges that come your way.",
    "Your journey is a testament to your resilience and bravery.",
    "You are capable of breaking free from the grip of OCD.",
    "I'm inspired by your dedication to therapy and self-improvement.",
    "You have the power to take control of your thoughts and actions.",
    "Your efforts in therapy are making a positive impact on your life.",
    "You are capable of finding peace and calm within yourself.",
    "I admire your willingness to face your fears and uncertainties.",
    "You are on the path to reclaiming your life from OCD.",
    "Your journey is filled with growth and self-discovery.",
    "I see your determination, and I know you can overcome this.",
    "You are not defined by OCD; you are so much more.",
    "Your journey towards healing is worth celebrating.",
    "I'm here to support you through the ups and downs of therapy.",
    "You have the strength to challenge and reframe negative thoughts.",
    "Your commitment to change is admirable.",
    "I'm impressed by your resilience in the face of challenges.",
    "You are creating a brighter future for yourself through therapy.",
    "Your progress may be gradual, but it is significant.",
    "I'm confident that you can conquer OCD and live a fulfilling life.",
    "You are not your thoughts; you have the power to choose how to respond.",
    "Your determination is inspiring those around you.",
    "You are on a journey of self-empowerment and growth.",
    "I believe in your ability to achieve your therapy goals.",
    "You have the strength to cope with uncertainty and discomfort.",
    "Your efforts in therapy are leading to positive changes.",
    "You are building a foundation for a more balanced and peaceful life.",
    "I'm here to listen and support you every step of the way.",
    "You are making progress, even if it's not always visible.",
    "Your courage in facing OCD head-on is commendable.",
    "You are worthy of happiness and freedom from OCD's grip.",
    "I see your resilience and dedication, and it's inspiring.",
    "You are learning valuable skills to navigate life's challenges.",
    "Your journey towards healing is unique and powerful.",
    "I'm amazed by your strength and determination to get better.",
    "You have the power to create positive change in your life.",
    "You are brave for confronting your fears and anxieties.",
    "Your commitment to growth and healing is making a difference.",
    "I believe in your ability to break free from OCD's hold on you.",
    "You have the strength to cope with uncertainty and discomfort.",
    "Your journey towards healing is filled with hope and possibility.",
    "I'm here to support you as you learn and grow.",
    "You are stronger than OCD, and you will overcome it.",
    "I'm inspired by your dedication to your well-being and recovery.",
    "You are not defined by your struggles; you are defined by your resilience.",
    "Your journey is a testament to your strength and determination.",
    "I believe in your ability to overcome OCD and find peace within yourself."
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
