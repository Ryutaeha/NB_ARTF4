using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// SCV���Ͽ� ����ִ� ���� ���� Dictionary�� ��Ƶ� Ŭ����
/// </summary>
public class CSVData
{
    public Dictionary<string, float> version = new Dictionary<string, float> { };
    public Dictionary<int, string> gameOverScript = new Dictionary<int, string> { };
    public Dictionary<int, string> gameLoadingScript1 = new Dictionary<int, string> { };
    public Dictionary<int, string> gameLoadingScript2 = new Dictionary<int, string> { };
    //CSV���Ͽ� ��� �ϳ� �߰��� ���⿡ Dictionary �߰� �� RowCheck()�� �ش� ��� �������� ����ġ �� �ϳ� �߰��ϸ� �˴ϴ�.
}
public class CSVLoader
{
    /// <summary>
    /// ���� ���� �ѹ��� ������ �ٸ��� ������Ʈ �ϴ� �޼���
    /// </summary>
    /// <param name="csvFile"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    internal CSVData LordFile(TextAsset csvFile, float version)
    {
        //���ο�  CSVData ��ü ����
        CSVData data = new();

        //�� ���� �ٲ� ������ �ش� Dictionary�� �ٲ��ִ� Dictionary
        Dictionary<int, string> addDate = null;

        //CSV������ ������ ,�� ������
        string csvText = csvFile.text[..^1];

        //CSV������ �� ���� �߶� �迭�� �������
        string[] rows = csvText.Split(new char[] { '\n' });

        // rows�� for���� ���� �ݺ� ó��
        for (int i = 0; i < rows.Length; i++)
        {
            //rows�� �ٽ� �ѹ� , �� �������� �߶���
            string[] rowsValues = rows[i].Split(new char[] { ',' });

            // rowsValues�� ù�� �� CSV�� ù��° ���� �����ϸ� ó���� if��
            if (rowsValues[0] != "")
            {
                //json������ ������ csv������ ������ ��ġ�Ѵٸ� null�� ����
                if (i == 0 && float.Parse(rowsValues[2]) == version) return null;
                //�ƴ϶�� 0��° �ε����� ���� ���� ��ġ�� �ٲ��ִ� �ε��� ����
                RowCheck(data, rowsValues, ref addDate);
                // �Ʒ� �ڵ带 �������� �ʰ� ���� �ݺ��� ����
                continue;
            }
            //�������� �ʴ´ٸ� �ش� 1��° �ε����� 2��° �ε����� ���� Dictionary�� �����
            addDate.Add(int.Parse(rowsValues[1]), rowsValues[2][..^1]);
        }
        // �ش� for���� �ݺ��� ������ �ش� �����͸� ����
        return data;
    }

    /// <summary>
    /// �� ������ ��ġ�� �Ѿ �� ��ġ�� �ٲ��ִ� �޼���
    /// </summary>
    /// <param name="data"></param>
    /// <param name="rowValues"></param>
    /// <param name="addDate"></param>
    private void RowCheck(CSVData data, string[] rowValues, ref Dictionary<int, string> addDate)
    {
        // ����ġ�� �����ؾ��ϴµ� ����ü ��ĳ�ؾ��ϴ� �����.
        switch (rowValues[0])
        {
            case "Version":
                data.version.Add(rowValues[1], float.Parse(rowValues[2]));
                break;
            case "GameOverScript":
                addDate = data.gameOverScript;
                break;
            case "GameLoadingScript1":
                addDate = data.gameLoadingScript1;
                break;
            case "GameLoadingScript2":
                addDate = data.gameLoadingScript2;
                break;
        }
    }
}
