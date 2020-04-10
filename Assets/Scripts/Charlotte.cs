using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charlotte : NPC
{
    SMDialogueTrigger trigger;
    SMPlayerTurn playerTurn;
    int eventCounter = 1;
    int dialogueCounter = 1;
    private bool convo = false;
    public static char dialogueChoice;
    Canvas parent;

    static Dictionary<string, (string, int, int)> attacks = new Dictionary<string, (string, int, int)>
    {
        { "Woeful Screech", ("The monster lets out a high pitched, deafening screech", 2, -1) },
        { "Speechless Gambit", ("The monster hurls a megaphone at you", 0, -2) },
    };

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<SMPlayerStats>();
        enemy = GameObject.FindWithTag("NPC").GetComponent<SMNPCEntity>();
        trigger = GameObject.FindWithTag("NPC").GetComponent<SMDialogueTrigger>();
        playerTurn = GameObject.FindWithTag("Player").GetComponent<SMPlayerTurn>();
        parent = GameObject.Find("DialogueSystem").GetComponent<Canvas>();
    }

    private void Update()
    {
        if (convo)
        {
            string dialogueName = "dialogue" + dialogueCounter;
            Dictionary<string, (string[], bool)> eventSelect = events["event" + eventCounter].Item2;
            string[] toSay = { };
            if (dialogueCounter >= events["event" + eventCounter].Item1)
            {
                convo = false;
                parent.gameObject.SetActive(false);
                player.switchState(Transitions.Command.exitConvo);
            }
            else
            {
                switch (player.getState())
                {
                    case Transitions.ProcessState.noBattle:
                        toSay = eventSelect[dialogueName].Item1;
                        trigger.dialogue = new Dialogue("Charlotte", toSay);
                        if (eventSelect[dialogueName].Item2)
                        {
                            SMDialogueTrigger.turn = 1;
                        }
                        else
                        {
                            SMDialogueTrigger.turn = 0;
                            dialogueCounter++;
                        }
                        player.switchState(Transitions.Command.enterConvo);
                        break;
                    case Transitions.ProcessState.dialogueChoice:
                        dialogueChoice = playerTurn.CheckForChoice();
                        dialogueName = dialogueName + dialogueChoice;
                        toSay = eventSelect[dialogueName].Item1;
                        trigger.dialogue = new Dialogue("Charlotte", toSay);
                        SMDialogueTrigger.turn = 0;
                        dialogueCounter++;
                        player.switchState(Transitions.Command.makeChoice);
                        break;
                }
            }
        }
    }

    public override (int, int) Use(string moveName)
    {
        int anxiety = attacks[moveName].Item2;
        int will = attacks[moveName].Item3;
        player.adjustAnxiety(anxiety);
        player.adjustWill(will);
        string[] msg = new string[] { attacks[moveName].Item1 };
        trigger.TriggerDialogue(new Dialogue("", msg));
        player.switchState(Transitions.Command.enemyChoice);
        return (anxiety, will);
    }

    public override void Converse()
    {
        convo = true;
    }

    static string[] dialogue1 = { "Hey there! Would you be interested in hearing about the drama club on campus?",
    "You must be a first-year student, so it would be a great way for you to make some new friends while having fun!"};

    static string[] dialogue1a = { "Hehe, call it impressive intuition.",
    "Just kidding. I’m a second-year and I’ve never seen you around before, so I just assumed you were a new student.",
    "And judging by your reaction, I guess I was right.",
    "The drama club is completely friendly to first-years too, by the way. It’s a great way to get involved with others on campus early in your college career.",
    "Take it from someone who joined as a first-year herself!"};

    static string[] dialogue1b = { "Oh, don’t worry about that!",
    "We’re totally friendly to people who’ve never had experience working in theater beforehand, and we make sure the club is an accepting environment where people of all experience levels are welcome!",
    "And besides, drama isn’t just all about acting! There’s also costume design, makeup, the stage crew, and directing, if those sound more interesting to you.",
    "We’re more than happy to accommodate your interests and what you want to bring to the club!"};

    static string[] dialogue1c = { "Aww, really? Are you sure about that?",
    "I know this probably sounds cliché, but, it’s important to take your mind off of school work every once in a while and do something that allows you to express yourself. Being a part of a club is just one of the many ways you can do that.",
    "Being a college student can be stressful sometimes, and it’s important for our mental health that we let loose every once in a while.",
    "That being said, I strongly urge you to reconsider and join a club, even if it doesn’t end up being the drama club.",
    "N-Not to say we wouldn’t love to have you! Haha…",
    "The drama club recruiter laughs nervously."};

    static string[] dialogue2 = { "Anyways, let me tell you more about the drama club, just FYI.",
    "Our goal is to help you learn more about the different aspects of theater, which includes learning stage terminology, developing your acting skills, what goes behind set building, yadda-yadda.",
    "We meet Thursdays at 6 p.m. in one of the classrooms on the second floor of the main building.",
    "Furthermore, you’re not required to attend every meeting unless you’re a part of one of our semesterly productions. So, if you’re busy sometimes and can’t make it, that’s totally fine!",
    "And, just like any other club on campus, we have a president, vice president, treasurer, and secretary, which is who you’re speaking to right now!",
    "...!",
    "Oh shoot, that reminds me! I haven’t even introduced myself yet! Silly me!",
    "I’m Charlotte, and I’m the secretary of the drama club!"};

    static string[] dialogue2a = { "Wow...Elena? That’s such a pretty name!",
    "It’s nice to meet you, Elen-I mean Elly! Haha!"};

    static string[] dialogue2b = { "Ooh that was a good one...Miss President! Haha!",
    "But seriously, I wish we had something like a “boring club” on campus. I’d kill to spare an hour once a week to just sit around and do nothing, maybe take a nap.",
    "Just take a break from everything, ya know?"};

    static string[] dialogue2c = { "Mhm! Likewise!" };

    static string[] dialogue3 = { "Okay, it looks like I’ve taken up enough of your time. Here’s my phone number, just in case you think of any more questions later." };
    static string[] dialogue4 = { "There’s still plenty of time left to join clubs, so take some time to think about it.",
    "I really hope you’ll consider joining, though. It’s a lot of stress-free fun and a great way to make some friends. We would love to have more members too!",
    "Alright, well, catch ya around then!"};



    public static Dictionary<string, (string[], bool)> event1 = new Dictionary<string, (string[], bool)>
    {
        { "dialogue1", (dialogue1, true) },
        { "dialogue1a", (dialogue1a, false) },
        { "dialogue1b", (dialogue1b, false) },
        { "dialogue1c", (dialogue1c, false) },
        { "dialogue2", (dialogue2, true) },
        { "dialogue2a", (dialogue2a, false) },
        { "dialogue2b", (dialogue2b, false) },
        { "dialogue2c", (dialogue2c, false) },
        { "dialogue3", (dialogue3, false) },
        { "dialogue4", (dialogue4, false) },
    };
    public static Dictionary<string, (int, Dictionary<string, (string[], bool)>)> events = new Dictionary<string, (int, Dictionary<string, (string[], bool)>)>
    {
        {"event1", (4, event1) },
    };

}
