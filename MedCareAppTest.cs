using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Threading;

namespace MedCare.SystemTest
{
    [TestFixture]
    public class MedCareAppTest
    {
        #region Session Attributes
        private WindowsDriver<WindowsElement> session;
        private String appID = "4055915c-700b-4208-a37e-849b65527452_zgnc71k7pd6s8!App";
        private String localIP = "127.0.0.1";
        private String port = "4723";
        #endregion

        #region Tests Attributes
        private String REGISTRATION_CREATE_ACCOUNT_BUTTON= "Criar conta";
        private String REGISTRATION_NAME_FIELD = "Nome";
        private String REGISTRATION_CPF_FIELD = "Cpf";
        private String REGISTRATION_AGE_FIELD = "Idade";
        private String REGISTRATION_PHONE_FIELD = "Telefone";
        private String REGISTRATION_EMAIL_FIELD = "Email";
        private String REGISTRATION_PASSWORD_FIELD = "Senha";
        private String REGISTRATION_CONFIRM_PASSWORD_FIELD = "Confirme sua senha";
        private String REGISTRATION_SEND_BUTTON = "Enviar";
        private String LOGIN_EMAIL_FIELD = "TextBox";
        private String LOGIN_PASSWORD_FIELD = "PasswordBox";
        private String LOGIN_SIGNIN_BUTTON = "Entrar";
        private String LOGIN_INCORRECT_CREDENTIALS_MESSAGE = "Email ou senha inválido";
        private String HOME_NOTIFICATIONS_TITLE = "Notificações";
        private String NAVIGATE_TABS = "PivotItem";
        private String EXAMS_TITLE = "Exames";
        private String APPOINTMENTS_TITLE = "Consultas";
        private String PROCEDURES_ADD_BUTTON = "AddNewAppoimentBtn";
        private String PROCEDURES_SUMMARY_FIELD = "ProcedureTitleTextBox";
        private String PROCEDURES_PROFESSIONAL_EMAIL_FIELD = "ProfessionalEmailTextBox";
        private String PROCEDURES_PATIENT_EMAIL_FIELD = "PatientEmailTextBox";
        private String PROCEDURES_DESCRIPTION_FIELD = "DecriptionTextBox";
        private String PROCEDURES_START_DATE_SELECTOR = "StartDatePicker";
        private String PROCEDURES_FINAL_DATE_SELECTOR = "EndDatePicker";
        private String PROCEDURES_PRIORITY_COMBOBOX = "PriorityComboBox";
        private String PROCEDURES_USER_TYPE_COMBOBOX = "UserTypeComboBox";
        private String PROCEDURES_STATUS_CHECKBOX = "StatusCheckBox";
        private String PROCEDURES_SUBMIT_BUTTON = "AddNewAppoimentBtn";
        private String PROCEDURES_LIST_ITEMS = "ListViewItem";
        #endregion

        #region SetUp and TearDown
        [SetUp]
        public void SetUp()
        {
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability("app", appID);
            capabilities.AddAdditionalCapability("waitForAppLaunch", 10);
            session = new WindowsDriver<WindowsElement>(new Uri($"http://{localIP}:{port}"), capabilities);
            session.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            session.Quit();
            session = null;
        }
        #endregion

        #region Registration Tests
        [Test]
        public void RegistrationWithAnIncompatibilityPasswords()
        {
            WindowsElement createAccountButton = session.FindElementByName(REGISTRATION_CREATE_ACCOUNT_BUTTON);
            createAccountButton.Click();
            WindowsElement nameField = session.FindElementByName(REGISTRATION_NAME_FIELD);
            nameField.SendKeys("Yudi Playstation Dois");
            WindowsElement cpfField = session.FindElementByName(REGISTRATION_CPF_FIELD);
            cpfField.SendKeys("12345678900");
            WindowsElement ageField = session.FindElementByName(REGISTRATION_AGE_FIELD);
            ageField.SendKeys("28");
            WindowsElement phoneField = session.FindElementByName(REGISTRATION_PHONE_FIELD);
            phoneField.SendKeys("40028922");
            WindowsElement emailField = session.FindElementByName(REGISTRATION_EMAIL_FIELD);
            emailField.SendKeys("funk-do-yudi@gmail.com");
            WindowsElement passwordField = session.FindElementByName(REGISTRATION_PASSWORD_FIELD);
            passwordField.SendKeys("123456");
            WindowsElement confirmPasswordField = session.FindElementByName(REGISTRATION_CONFIRM_PASSWORD_FIELD);
            confirmPasswordField.SendKeys("654321");

            WindowsElement sendButton = session.FindElementByName(REGISTRATION_SEND_BUTTON);
            sendButton.Click();

            WindowsElement errorMessage = session.FindElementByName("Coloque senhas iguais!");
            Assert.IsTrue(errorMessage.Displayed);
        }
        #endregion

        #region Login Tests
        [Test]
        public void LoginInWithValidCredentials()
        {
            Login("patient@gmail.com", "123456");
            WindowsElement mainScreenTitle = session.FindElementByName(HOME_NOTIFICATIONS_TITLE);
            Assert.IsTrue(mainScreenTitle.Displayed);
        }

        [Test]
        public void LoginInWithInvalidCredentials()
        {
            Login("patient@gmail.com", "abcdef");
            WindowsElement exceptionMessage = session.FindElementByName(LOGIN_INCORRECT_CREDENTIALS_MESSAGE);
            Assert.IsTrue(exceptionMessage.Displayed);
        }
        #endregion

        #region Exams Tests
        [Test]
        public void AddingNewExam()
        {
            //Login("patient@gmail.com", "123456");

            //goToPage(2);

            //fillTheInformationsOfProcedure();

            //WindowsElement submitButton = session.FindElementById(PROCEDURES_SUBMIT_BUTTON);
            //submitButton.Click();

            //int ExamsInList = session.FindElementsByClassName(PROCEDURES_LIST_ITEMS).Count;
            //Assert.AreNotEqual(ExamsInList, 0);
        }
        #endregion

        #region Appointments Tests
        [Test]
        public void AddingNewAppointment()
        {
            //Login("patient@gmail.com", "123456");

            //goToPage(1);

            //fillTheInformationsOfProcedure();

            //WindowsElement submitButton = session.FindElementById(PROCEDURES_SUBMIT_BUTTON);
            //submitButton.Click();

            //int AppointmentsInList = session.FindElementsByClassName(PROCEDURES_LIST_ITEMS).Count;
            //Assert.AreNotEqual(AppointmentsInList, 0);
        }
        #endregion

        #region Schedule Tests
        [Test]
        public void selectDate()
        {
            Login("patient@gmail.com", "123456");

            goToPage(3);

            WindowsElement daySixTeen = session.FindElementByName("16");
            daySixTeen.Click();
            Assert.IsTrue(daySixTeen.Selected);
        }
        #endregion

        #region Navigate Tests 
        [Test]
        public void navigateOfAllApplication()
        {
            Login("patient@gmail.com", "123456");

            goToPage(1);
            goToPage(2);
            goToPage(3);
            goToPage(4);
            goToPage(0);

            WindowsElement mainScreenTitle = session.FindElementByName(HOME_NOTIFICATIONS_TITLE);
            Assert.IsTrue(mainScreenTitle.Displayed);
        }
        #endregion

        #region General Methods
        private void Login(string email, string password)
        {
            WindowsElement loginField = session.FindElementByClassName(LOGIN_EMAIL_FIELD);
            loginField.SendKeys(email);
            WindowsElement passwordField = session.FindElementByClassName(LOGIN_PASSWORD_FIELD);
            passwordField.SendKeys(password);
            WindowsElement signinButton = session.FindElementByName(LOGIN_SIGNIN_BUTTON);
            signinButton.Click();
        }

        private void goToPage(int page)
        {
            WindowsElement proceduresTab = session.FindElementsByClassName(NAVIGATE_TABS)[page];
            proceduresTab.Click();
        }

        private void fillTheInformationsOfProcedure()
        {
            WindowsElement addExamButton = session.FindElementById(PROCEDURES_ADD_BUTTON);
            addExamButton.Click();

            WindowsElement summaryField = session.FindElementById(PROCEDURES_SUMMARY_FIELD);
            summaryField.Clear();
            summaryField.SendKeys("Exame x");

            WindowsElement professionalEmailField = session.FindElementById(PROCEDURES_PROFESSIONAL_EMAIL_FIELD);
            professionalEmailField.Clear();
            professionalEmailField.SendKeys("professional@gmail.com");

            WindowsElement patientEmailField = session.FindElementById(PROCEDURES_PATIENT_EMAIL_FIELD);
            patientEmailField.Clear();
            patientEmailField.SendKeys("patient@gmail.com");

            WindowsElement descriptionField = session.FindElementById(PROCEDURES_DESCRIPTION_FIELD);
            descriptionField.Clear();
            descriptionField.SendKeys("Serao realizados y procedimentos neste exame");


            WindowsElement startDateSelector = session.FindElementById(PROCEDURES_START_DATE_SELECTOR);
            startDateSelector.Click();
            session.FindElementByName("23").Click();

            WindowsElement finalDateSelector = session.FindElementById(PROCEDURES_FINAL_DATE_SELECTOR);
            finalDateSelector.Click();
            session.FindElementByName("23").Click();

            WindowsElement priorityCombobox = session.FindElementsByClassName(PROCEDURES_PRIORITY_COMBOBOX)[0];
            priorityCombobox.Click();

            WindowsElement typeCombobox = session.FindElementsByClassName(PROCEDURES_USER_TYPE_COMBOBOX)[0];
            typeCombobox.Click();

            WindowsElement statusCheckbox = session.FindElementById(PROCEDURES_STATUS_CHECKBOX);
            statusCheckbox.Click();
        }
        #endregion

    }
}




