using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class JsonLoader
{
    // json �ε带 ���� �����н�
    static string filePath = Path.Combine(Application.dataPath, "@Resources/@Json/GameUIScript.json");
    
    
    /// <summary>
    /// ���� �н��� ������ ���̽� ������ �о���� �޼���
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    private string ReadJsonFile(string filePath)
    {
        try
        {
            //���� �н��� ������ ���� �������µ� �����ϸ� json�� ��� ���� string���� �Ľ�
            return File.ReadAllText(filePath);
        }
        catch (Exception e)
        {
            //�����ϸ� ���� �޽��� ��� �� null����
            Debug.LogError("Error reading JSON file: " + e.Message);
            return null;
        }
    }
  
    
    /// <summary>
    /// �����Ͱ� ���� �ƴ϶�� ������ �� �����͸� ������ ���� json ������ ���� ���� �޼���
    /// </summary>
    /// <param name="updateDate"></param>
    internal void JsonLoad(CSVData updateDate)
    {
        // ���� CSVData ������ json���Ϸ� ����ȭ
        string json = JsonConvert.SerializeObject(updateDate, Formatting.Indented);

        using (StreamWriter file = File.CreateText(filePath))
        {
            //StreamWriter�� ����Ͽ� json �ۼ�
            file.Write(json);
        }
    }

    /// <summary>
    /// json ������ ������ Ȯ�� �ϴ� �޼���
    /// </summary>
    /// <returns></returns>
    internal float JsonVersionLoad()
    {
        // ���� �н��� �̿��Ͽ� json ���� string���� ��������
        string jsonContent = ReadJsonFile(filePath);

        // �ش� ���� ������� �ʴٸ� ����
        if (!string.IsNullOrEmpty(jsonContent))
        {
            // JObject�� ��� �����͸� ��ȯ
            JObject jsonObject = JObject.Parse(jsonContent);

            // JObject�� Ű���� �̿��Ͽ� ���� ����
            float versionValue = jsonObject["version"]["CurrentVersion"].Value<float>();

            // ����
            return versionValue;
        }
        // �ƴҽ� 0����
        return 0;
    }


    /// <summary>
    /// json���Ͽ� Ű���� ������ �ش� �ȿ� �ִ� ���� Dictionary<int, string> Ÿ������ �������� �޼��� ������ null�� ��ȯ
    /// </summary>
    /// <param name="dataName"></param>
    /// <returns></returns>
    internal Dictionary<int, string> JsonDataLoad(string dataName)
    {
        // ���� ���� Dictionary ����
        Dictionary<int, string> result = null;

        // json���� �ε�
        string jsonContent = ReadJsonFile(filePath);

        // ���� �ƴ϶�� ����
        if (!string.IsNullOrEmpty(jsonContent))
        {
            // JObject�� ��� �����͸� ��ȯ
            JObject jsonObject = JObject.Parse(jsonContent);

            // �޾ƿ� ���� �̿��Ͽ� �ش� ���� ��ġ�ϴ� �����͸� JObject�� ������
            JObject gameLoadingScript1Object = (JObject)jsonObject[dataName];

            // gameLoadingScript1Object�� Dictionary<int, string>���� �Ľ��� result�� �Ҵ�
            result = gameLoadingScript1Object.ToObject<Dictionary<int, string>>();

        }
        // ������ null ������ Dictionary�� ��ȯ
        return result;
    }
}
