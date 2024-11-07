using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;

public class Login : MonoBehaviour
{

    [SerializeField] string email;
    [SerializeField] string password;

    public InputField inputTextEmail;
    public InputField inputTextPassword;
    public Text loginResult;



    FirebaseAuth auth;
    void Awake()
    {
        // �ʱ�ȭ
        auth = FirebaseAuth.DefaultInstance;
    }


    // ��ư�� ������ ������ �Լ�.
    public void JoinBtnOnClick()
    {
        email = inputTextEmail.text;
        password = inputTextPassword.text;

        Debug.Log("email: " + email + ", password: " + password);

        CreateUser();
    }


    void CreateUser()
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                loginResult.text = "ȸ������ ����";
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                loginResult.text = "ȸ������ ����";
                return;
            }

            // Firebase user has been created.

            Firebase.Auth.AuthResult newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.User.DisplayName, newUser.User.UserId);

            loginResult.text = "ȸ������ �·�";
        });
    }
    public void LoginBtnOnClick()
    {
        email = inputTextEmail.text;
        password = inputTextPassword.text;

        Debug.Log("email: " + email + ", password: " + password);

        LoginUser();
        LoadScene.instance.NextScene("Main");
    }

    void LoginUser()
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                loginResult.text = "�α��� ����";
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                loginResult.text = "�α��� ����";
                return;
            }

            Firebase.Auth.AuthResult newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
            newUser.User.DisplayName, newUser.User.UserId);
            LoadScene.instance.NextScene("Main");

        });

    }
    }