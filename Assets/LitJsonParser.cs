using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.Profiling;

public class LitJsonParser : MonoBehaviour {

    //public string jsonText;
    [Header("데이터가 있는 폴더의 경로를 넣으시오.")]
    public string path;

    int fileIndex = 0;

    //[SerializeField]
    List<PoseData> currentPoseDatas;

    readonly int scanResWidth = 1920;
    readonly int scanResHeight = 1080;
    float scale = 0.02f;

    public readonly bool logEnabled = false;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {

        Profiler.BeginSample("ReadFile");
        string poseText = ReadFile(path);
        Profiler.EndSample();

        Profiler.BeginSample("ReadPose");
        if (poseText != "")
        {
            currentPoseDatas = ReadPose(poseText);
            fileIndex++;
        }
        Profiler.EndSample();
    }

    private void OnDrawGizmos()
    {
        DebugDrawPose(currentPoseDatas);
    }

    void OnGUI()
    {
        ScreenText.SetAll(15, TextAnchor.UpperLeft, Color.black);
        ScreenText.DrawFPS();
    }

    List<PoseData> ReadPose(string text) //json 텍스트에서 포즈 읽어오기
    {
        LitJson.JsonData jsonData = LitJson.JsonMapper.ToObject(text);
        List<PoseData> poseDatas = new List<PoseData>();

        int personIndex = -1;
        int kpIndex = -1;
        string debugText = "";
        List<string> debugTextList = new List<string>();

        //Version
        //print(string.Format("version : {0}", poseData["version"]));

        //Keypoints
        foreach (LitJson.JsonData people in (jsonData["people"] as IList))
        {
            personIndex++;

            PoseData personPoseData = new PoseData();

            /*
            foreach (LitJson.JsonData kp in (people["pose_keypoints_2d"] as IList))
            {
                kpIndex++;

                //Make debug text
                if (logEnabled)
                {
                debugText = debugText + string.Format(
                    "[person <b>{0}</b> / kp <b>{1}</b>] : <b>{2}</b>\n",
                    personIndex, kpIndex, kp);
                }

                // Add poseData
                if (kpIndex % 3 == 0)
                {
                    //print(kp.GetJsonType().ToString());
                    if (kp.GetJsonType() == JsonType.Double)
                    {
                        personPoseData.positions[kpIndex / 3].x = (float)(double)kp;
                    }
                    else if (kp.GetJsonType() == JsonType.Int)
                    {
                        personPoseData.positions[kpIndex / 3].x = (int)kp;
                    }
                    //print("x " + personPoseData.positions[personIndex / 3].x);
                    //personPoseData.positions[personIndex / 3].x = (float)(double)kp;
                    }
                if (kpIndex % 3 == 1)
                {
                    if (kp.GetJsonType() == JsonType.Double)
                    {
                        personPoseData.positions[kpIndex / 3].y = (float)(double)kp;
                    }
                    else if (kp.GetJsonType() == JsonType.Int)
                    {
                        personPoseData.positions[kpIndex / 3].y = (int)kp;
                    }
                    //print("y " + personPoseData.positions[personIndex / 3].y);
                    //personPoseData.positions[personIndex / 3].y = (float)(double)kp;
                }
                if (kpIndex % 3 == 2)
                {
                    if (kp.GetJsonType() == JsonType.Double)
                    {
                        personPoseData.confidences[kpIndex / 3] = (float)(double)kp;
                    }
                    else if (kp.GetJsonType() == JsonType.Int)
                    {
                        personPoseData.confidences[kpIndex / 3] = (int)kp;
                    }
                    //print("conf " + personPoseData.confidences[personIndex / 3]);
                    //personPoseData.confidences[personIndex / 3] = (float)(double)kp;
                }
            }
            */

            foreach (LitJson.JsonData kp in (people["pose_keypoints_2d"] as IList)) {; }
            for(int i = 0; i < (people["pose_keypoints_2d"] as IList).Count; i++)
            {
                LitJson.JsonData kp = (LitJson.JsonData)(people["pose_keypoints_2d"] as IList)[i];
                kpIndex++;

                //Make debug text
                if (logEnabled)
                {
                    debugTextList.Add(string.Concat(debugText, string.Format(
                        "[person <b>{0}</b> / kp <b>{1}</b>] : <b>{2}</b>\n",
                        personIndex, kpIndex, kp)));
                    
                    //debugText = string.Concat( debugText , string.Format(
                    //    "[person <b>{0}</b> / kp <b>{1}</b>] : <b>{2}</b>\n",
                    //    personIndex, kpIndex, kp));
                }

                // Add poseData
                if (kpIndex % 3 == 0)
                {
                    //print(kp.GetJsonType().ToString());
                    if (kp.GetJsonType() == JsonType.Double)
                    {
                        personPoseData.positions[kpIndex / 3].x = (float)(double)kp;
                    }
                    else if (kp.GetJsonType() == JsonType.Int)
                    {
                        personPoseData.positions[kpIndex / 3].x = (int)kp;
                    }
                    //print("x " + personPoseData.positions[personIndex / 3].x);
                    //personPoseData.positions[personIndex / 3].x = (float)(double)kp;
                }
                if (kpIndex % 3 == 1)
                {
                    if (kp.GetJsonType() == JsonType.Double)
                    {
                        personPoseData.positions[kpIndex / 3].y = (float)(double)kp;
                    }
                    else if (kp.GetJsonType() == JsonType.Int)
                    {
                        personPoseData.positions[kpIndex / 3].y = (int)kp;
                    }
                    //print("y " + personPoseData.positions[personIndex / 3].y);
                    //personPoseData.positions[personIndex / 3].y = (float)(double)kp;
                }
                if (kpIndex % 3 == 2)
                {
                    if (kp.GetJsonType() == JsonType.Double)
                    {
                        personPoseData.confidences[kpIndex / 3] = (float)(double)kp;
                    }
                    else if (kp.GetJsonType() == JsonType.Int)
                    {
                        personPoseData.confidences[kpIndex / 3] = (int)kp;
                    }
                    //print("conf " + personPoseData.confidences[personIndex / 3]);
                    //personPoseData.confidences[personIndex / 3] = (float)(double)kp;
                }
            }

            kpIndex = -1;

            poseDatas.Add(personPoseData);
        }

        //Print debug log
        if (logEnabled)
        {
            debugText = string.Concat(debugTextList.ToArray());
            print(debugText);
        }
        
        return poseDatas;

        //print(string.Format("kp : {0}", poseData["people"][0]["pose_keypoints_2d"][0]));
    }

    string ReadFile(string directory) //디렉토리에서 json 파일 읽어오기
    {
        //"000000000001_keypoints.json"

        string pathFile = "";
        string text = "";

        //Make file path string
        string fileIndexStr = fileIndex.ToString();

        for (int i = 12 - fileIndexStr.Length; i > 0; i--)
        {
            pathFile = pathFile + "0";
        }
        pathFile = directory + Path.DirectorySeparatorChar + pathFile + fileIndexStr + "_keypoints.json";

        if (!File.Exists(pathFile))
        {
            Debug.LogWarning("Cannot Find " + pathFile);
            return "";
        }
        else
        {
            //Read file
            text = System.IO.File.ReadAllText(pathFile);
            
        }
        return text;


        //System.IO.File.ReadAllText()

        //StreamReader reader = new StreamReader(path);
        //Debug.Log(reader.ReadToEnd());
        //reader.Close();

    }

    void DebugDrawPose(List<PoseData> poseDatas) // Scene 뷰에서 각 점의 위치 출력
    {
        
        if (poseDatas != null)
        {
            //draw border
            Gizmos.color = new Color(1f, 1f, 1f);

            Gizmos.DrawLine(
                new Vector3(-scanResWidth/ 2.0f * scale, -scanResHeight / 2.0f * scale, 0),
                new Vector3(-scanResWidth / 2.0f * scale, scanResHeight / 2.0f * scale, 0));
            Gizmos.DrawLine(
                new Vector3(-scanResWidth / 2.0f * scale, scanResHeight / 2.0f * scale, 0),
                new Vector3(scanResWidth / 2.0f * scale, scanResHeight / 2.0f * scale, 0));
            Gizmos.DrawLine(
                new Vector3(scanResWidth / 2.0f * scale, scanResHeight / 2.0f * scale, 0),
                new Vector3(scanResWidth / 2.0f * scale, -scanResHeight / 2.0f * scale, 0));
            Gizmos.DrawLine(
                new Vector3(scanResWidth / 2.0f * scale, -scanResHeight / 2.0f * scale, 0),
                new Vector3(-scanResWidth / 2.0f * scale, -scanResHeight / 2.0f * scale, 0));
            
            //draw pose
            for (int i = 0; i < poseDatas.Count; i++)
            {
                for (int j = 0; j < poseDatas[i].positions.Length; j++)
                {
                    //print(i + "/ " + j + "/ " + poseDatas[i].positions[j]);
                    
                    poseDatas[i].positionsMapped[j] = new Vector2(
                        (poseDatas[i].positions[j].x - scanResWidth / 2.0f) * scale,
                        (poseDatas[i].positions[j].y - scanResHeight / 2.0f) * scale);

                    Gizmos.color = new Color(poseDatas[i].confidences[j], poseDatas[i].confidences[j], poseDatas[i].confidences[j]);

                    Gizmos.DrawSphere(new Vector3(poseDatas[i].positionsMapped[j].x,
                        poseDatas[i].positionsMapped[j].y), 0.2f);
                    //Debug.Log(poseDatas[i].positions[j]);
                }

                Color personLineCol;
                switch (i)
                {
                    case 0:
                        {
                            personLineCol = Color.red; break;
                        }
                    case 1:
                        {
                            personLineCol = Color.green; break;
                        }
                    case 2:
                        {
                            personLineCol = Color.blue; break;
                        }
                    case 3:
                        {
                            personLineCol = Color.yellow; break;
                        }
                    default:
                        {
                            personLineCol = Color.black; break;
                        }
                }

                Gizmos.color = personLineCol;

                Gizmos.DrawLine(poseDatas[i].positionsMapped[0], poseDatas[i].positionsMapped[1]);
                Gizmos.DrawLine(poseDatas[i].positionsMapped[1], poseDatas[i].positionsMapped[2]);
                Gizmos.DrawLine(poseDatas[i].positionsMapped[2], poseDatas[i].positionsMapped[3]);
                Gizmos.DrawLine(poseDatas[i].positionsMapped[3], poseDatas[i].positionsMapped[4]);
                Gizmos.DrawLine(poseDatas[i].positionsMapped[1], poseDatas[i].positionsMapped[5]);
                Gizmos.DrawLine(poseDatas[i].positionsMapped[5], poseDatas[i].positionsMapped[6]);
                Gizmos.DrawLine(poseDatas[i].positionsMapped[6], poseDatas[i].positionsMapped[7]);
                Gizmos.DrawLine(poseDatas[i].positionsMapped[1], poseDatas[i].positionsMapped[8]);
                Gizmos.DrawLine(poseDatas[i].positionsMapped[8], poseDatas[i].positionsMapped[9]);
                Gizmos.DrawLine(poseDatas[i].positionsMapped[9], poseDatas[i].positionsMapped[10]);
                Gizmos.DrawLine(poseDatas[i].positionsMapped[10], poseDatas[i].positionsMapped[11]);
                Gizmos.DrawLine(poseDatas[i].positionsMapped[8], poseDatas[i].positionsMapped[12]);
                Gizmos.DrawLine(poseDatas[i].positionsMapped[12], poseDatas[i].positionsMapped[13]);
                Gizmos.DrawLine(poseDatas[i].positionsMapped[13], poseDatas[i].positionsMapped[14]);
                Gizmos.DrawLine(poseDatas[i].positionsMapped[0], poseDatas[i].positionsMapped[15]);
                Gizmos.DrawLine(poseDatas[i].positionsMapped[0], poseDatas[i].positionsMapped[16]);
                Gizmos.DrawLine(poseDatas[i].positionsMapped[15], poseDatas[i].positionsMapped[17]);
                Gizmos.DrawLine(poseDatas[i].positionsMapped[16], poseDatas[i].positionsMapped[18]);

            }
        }



    }

    [System.Serializable]
    public class PoseData // 한 사람의 포즈 데이터를 가짐
    {
        public Vector2[] positions;
        public Vector2[] positionsMapped;
        public float[] confidences;

        public PoseData()
        {
            positions = new Vector2[25];
            positionsMapped = new Vector2[25];
            confidences = new float[25];
        }
    }
}
