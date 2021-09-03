using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using TMPro;

public class ValidateInputField : MonoBehaviour
{
    public GameObject InputBirthText;
    public GameObject InputDeathText;
    public GameObject InputNameText;
    public GameObject InputRelationText;

    public GameObject NextButton;
    private bool is_nextable;

    private void Start()
    {
        is_nextable = true; // ���߿� �ʱⰪ false�� �����ϰ�, �Է°� ���� ����
    }
    void Update()
    {
        
        TextMeshProUGUI InputBirthTextField = InputBirthText.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI InputDeathTextField = InputDeathText.GetComponent<TextMeshProUGUI>();
/*      // ���Խ����� validation �Ҷ������� ������ �ڵ��Դϴ�^^ by.����
        if (Regex.IsMatch(InputBirthTextField.text, @"^(19|20)\d{2}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[0-1])$"))
        {
            Debug.Log("ismatched");
            if (Regex.IsMatch(InputDeathTextField.text, @"^(19|20)\d{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[0-1])$"))
            {
                NextButton.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Tags/Rectangle 51");
                is_nextable = true;
            }
            else
            {
                NextButton.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Tags/BOGEUMRegisterNextButton");
                is_nextable = false;
            }
        }
        else
        {
            NextButton.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Tags/BOGEUMRegisterNextButton");
            is_nextable = false;
        }
*/

    }
    public void TakeNextStep()
    {
        if (is_nextable)
        {
            NewBOGEUM2Server infoBOGEUM = GameObject.Find("Send2Server").GetComponent<NewBOGEUM2Server>();
            infoBOGEUM.deceasedBirth = InputBirthText.GetComponent<TextMeshProUGUI>().text;
            infoBOGEUM.deceasedBirth = InputDeathText.GetComponent<TextMeshProUGUI>().text;
            infoBOGEUM.deceasedName = InputNameText.GetComponent<TextMeshProUGUI>().text;
            infoBOGEUM.relation = InputRelationText.GetComponent<TextMeshProUGUI>().text;
            GameObject.Find("WritesInfo").transform.Find("View").gameObject.SetActive(false);
            GameObject.Find("SelectPhoto").transform.Find("View").gameObject.SetActive(true);
        }
    }
}
