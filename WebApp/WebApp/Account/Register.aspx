<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebApp.Account.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="z-index: 1; left: 440px; top: 517px; position: absolute; height: 498px; width: 1167px">
    <asp:CreateUserWizard ID="CreateUserWizard2" runat="server" style="z-index: 1; left: 16px; top: 29px; position: absolute; height: 293px; width: 267px; font-weight: 700;" CompleteSuccessText="A sua conta foi criada com sucesso!" ConfirmPasswordCompareErrorMessage="A password e confirmação de password têm de ser iguais!" ConfirmPasswordLabelText="Confirmar Password:" ConfirmPasswordRequiredErrorMessage="A confirmação de password é obrigatória" ContinueButtonText="Continuar" ContinueDestinationPageUrl="~/Default.aspx" CreateUserButtonText="Criar Conta" DuplicateEmailErrorMessage="A password que inseriu já está em uso! Escolha uma diferente" DuplicateUserNameErrorMessage="Escolha um username diferente" EmailRegularExpressionErrorMessage="Insira um endereço e-mail" EmailRequiredErrorMessage="É obrigatório inserir um endereço e-mail" EnableTheming="True" InvalidAnswerErrorMessage="Escolha uma pergunta de segurança diferente" InvalidEmailErrorMessage="Escolha um endereço e-mail" InvalidQuestionErrorMessage="Insira uma pergunta de segurança" PasswordRegularExpressionErrorMessage="Escolha uma password diferente" PasswordRequiredErrorMessage="Obrigatório inserir Password" QuestionLabelText="Pergunta de segurança:" QuestionRequiredErrorMessage="Obrigatório inserir uma pergunta de segurança" UnknownErrorMessage="A sua conta não foi criada! Tente de novo" UserNameRequiredErrorMessage="Obrigatório inserir username">
        <WizardSteps>
            <asp:CreateUserWizardStep runat="server" />
            <asp:CompleteWizardStep runat="server" />
        </WizardSteps>
    </asp:CreateUserWizard>
</div>
</asp:Content>
