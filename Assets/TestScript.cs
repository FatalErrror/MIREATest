using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    [Header("Main")]
    public bool IsObjectMode;

    public Material CubeMaterial;

    [Header("UI")]
    public Text LogText;
    public Text ModeText;

    public float LogTime = 2.5f;


    private GameObject _cube;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) IsObjectMode = !IsObjectMode;

        if (IsObjectMode) ModeText.text = "Включен объектный режим";
        else ModeText.text = "Включен компонентный режим";

        if (Input.GetKeyDown(KeyCode.Space)) Spacekey();

        if (Input.GetKeyDown(KeyCode.I)) Ikey();

        if (Input.GetKeyDown(KeyCode.D)) Dkey();

        if (Input.GetKeyDown(KeyCode.E)) Ekey();
    }

    public void Spacekey()
    {
        if (IsObjectMode)
        {
            if (_cube) Destroy(_cube);
            else Log("Нельзя удалить несуществующий куб");
        }
        else
        {
            if (_cube)
            {
                if (_cube.TryGetComponent<MeshRenderer>(out var mesh)) Destroy(mesh);
                else Log("Нельзя удалить несуществующий компонент");
            }
            else Log("Нельзя удалить компонент так как куб несуществует");
        }
    }

    public void Ikey()
    {
        if (IsObjectMode)
        {
            if (!_cube)
            {
                _cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                _cube.GetComponent<MeshRenderer>().material = CubeMaterial;
            }
            else Log("Нельзя создать уже созданный куб");
        }
        else
        {
            if (_cube)
            {
                if (!_cube.TryGetComponent<MeshRenderer>(out var mesh)) _cube.AddComponent<MeshRenderer>().material = CubeMaterial;
                else Log("Нельзя добавить компонент так как он уже существует");
            }
            else Log("Нельзя добавить компонент так как куб несуществует");
        }
    }

    public void Dkey()
    {
        if (IsObjectMode)
        {
            if (_cube)
            {
                if (_cube.activeSelf) _cube.SetActive(false);
                else Log("Нельзя деактивировать деактивированный куб");
            }
            else Log("Нельзя деактивировать куб так как куб несуществует");
        }
        else
        {
            if (_cube)
            {
                if (_cube.TryGetComponent<MeshRenderer>(out var mesh))
                {
                    if (mesh.enabled) mesh.enabled = false;
                    else Log("Нельзя деактивировать деактивированный компонент");
                }
                else Log("Нельзя деактивировать несуществующий компонент");
            }
            else Log("Нельзя деактивировать компонент так как куб несуществует");
        }
    }

    public void Ekey()
    {
        if (IsObjectMode)
        {
            if (_cube)
            {
                if (!_cube.activeSelf) _cube.SetActive(true);
                else Log("Нельзя активировать активированный куб");
            }
            else Log("Нельзя активировать куб так как куб несуществует");
        }
        else
        {
            if (_cube)
            {
                if (_cube.TryGetComponent<MeshRenderer>(out var mesh))
                {
                    if (!mesh.enabled) mesh.enabled = true;
                    else Log("Нельзя активировать активированный компонент");
                }
                else Log("Нельзя активировать несуществующий компонент");
            }
            else Log("Нельзя активировать компонент так как куб несуществует");
        }
    }

    

    private void Log(string message)
    {
        LogText.gameObject.SetActive(true);
        LogText.text = message;
        StartCoroutine(DisactiveLog());
    }

    private IEnumerator DisactiveLog()
    {
        yield return new WaitForSecondsRealtime(LogTime);
        LogText.gameObject.SetActive(false);
    }


}
