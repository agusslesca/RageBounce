using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    [Header("UI")]
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    

    [Header("Opciones")]
    public string savedUsername = "player"; // usuario de ejemplo
    public string savedPassword = "1234";   // contrase�a de ejemplo
    public string sceneToLoad = "Juego";    // escena al iniciar sesi�n

    public void OnLoginButton()
    {
        string usuario = usernameInput.text;
        string contrasena = passwordInput.text;

        Debug.Log("Usuario: " + usuario);
        Debug.Log("Contrase�a: " + contrasena);

        if (usuario == savedUsername && contrasena == savedPassword)
        {
            Debug.Log("Login exitoso!");
            
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.Log("Usuario o contrase�a incorrectos.");
            
        }
    }
}
