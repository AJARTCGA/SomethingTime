using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour {
    public int mControllerNum = 1;
    public Text mHatText;
    public Text mAccessoryText;
    public Text mPoseText;
    public GameObject mHead;
    int mHatSelect = 0;
    int mAccessorySelect = 0;
    int mPoseSelect = 0;

    int[] mHats = {8,3,7,2,0 };
    int[] mAccesories = { 5,6,2,8};
    int[] mPose = {1,4,6,12,6,32,5 };
    int mCostumeType = 0; //0 = hat 1 = accessory 2 = pose
    const float delay = .3f;
    float mDelay = .3f;
    Vector3 mHeadPos;
    static Color sHatColor = new Color(250 / 255.0f, 150 / 255.0f, 50 / 255.0f);
    static Color sAccessoryColor = new Color(103 / 255.0f, 18 / 255.0f, 154 / 255.0f);
    static Color sPoseColor = new Color(200 / 255.0f, 100 / 255.0f, 170 / 255.0f);

    // Use this for initialization
    void Start () {
        mHeadPos = mHead.transform.position;
        // Vector3 GameObjec 	
    }
	
	// Update is called once per frame
	void Update () {
        mHatText.text = "Hat: " + mHatSelect.ToString();
        mAccessoryText.text = "Accessory: " + mAccessorySelect.ToString();
        mPoseText.text = "Pose: " + mPoseSelect.ToString();
        mHeadPos = mHead.transform.position;
        float x = Input.GetAxis("Player" + mControllerNum + "X");
        float z = Input.GetAxis("Player" + mControllerNum + "Y");
        if (Input.GetButton("Start"))
        {
            StartGame();
        }
        if (mDelay < 0)
        {
            if (x >= 0.01 || x <= -0.01 || z > 0.01 || z <= -0.01)
            {

                if (x > 0)
                {
                    if (mCostumeType == 0)
                    {
                        mHatSelect++;
                        if (mHatSelect > mHats.Length - 1)
                            mHatSelect = 0;
                            
                    }
                    if (mCostumeType == 1)
                    {
                        mAccessorySelect++;
                        if (mAccessorySelect > mAccesories.Length - 1)
                            mAccessorySelect = 0;
                            
                    }
                    if (mCostumeType == 2)
                    {
                        mPoseSelect++;
                        if (mPoseSelect > mPose.Length - 1)
                            mPoseSelect = 0;                          
                    }

                }

                else if (x < 0)
                {
                    
                    if (mCostumeType == 0)
                    {
                        mHatSelect--;
                        if (mHatSelect < 0)
                            mHatSelect = mHats.Length - 1;
                            
                    }
                    if (mCostumeType == 1)
                    {
                        mAccessorySelect--;
                        if (mAccessorySelect < 0)
                            mAccessorySelect = mAccesories.Length - 1;
                            
                    }
                    if (mCostumeType == 2)
                    {
                        mPoseSelect--;
                        if (mPoseSelect < 0)
                            mPoseSelect = mPose.Length - 1;                            
                    }
                }

                else if (z > 0.01)
                {
                    mCostumeType--;
                    if (mCostumeType < 0)
                    {
                        mCostumeType = 2;
                    }
                }

                else if (z < -0.01)
                {
                    mCostumeType++;
                    if (mCostumeType > 2)
                    {
                        mCostumeType = 0;
                    }
                }

                mDelay = delay;
            }
        }//end input if
        mDelay -= Time.deltaTime;
        colorText();
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    void colorText()
    {
        mHatText.color = Color.black;
        mAccessoryText.color = Color.black;
        mPoseText.color = Color.black;
        switch(mCostumeType)
        {
            case 0:
                mHatText.color = sHatColor;
                break;
            case 1:
                mAccessoryText.color = sAccessoryColor;
                break;
            case 2:
                mPoseText.color = sPoseColor;
                break;

        }
    }
}



