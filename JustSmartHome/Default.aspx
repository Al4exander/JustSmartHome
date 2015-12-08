<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="JustSmartHome.MainForm" %>

<!DOCTYPE html>
	<html xmlns="http://www.w3.org/1999/xhtml">
		<head>
			<title>Умный дом</title>
            <link rel="stylesheet" href="myStyles.css"   />
            <script type="text/javascript">
            function timer(){
                var obj=document.getElementById('timer_inp');
                obj.innerHTML--;
                if (obj.innerHTML==0){
                alert("Микроволновка закончила приготовление!");
                setTimeout(function(){},1000);
                }
                else
                {
                    setTimeout(timer,1000);
                }
            }
            setTimeout(timer,1000);
            </script>
		</head>
	<body class="myBackground">
		<form id="form1" runat ="server">
            <div>
                <center><img src="Images/logo.png" style="height: 150px; width: 460px"/></center>
                    <center>
                        <asp:ImageButton ImageUrl="Images/add_lamp.png" ID="AddLamp" runat="server" CssClass="sendsubmit"/>
                        <asp:ImageButton ImageUrl="Images/add_alarm.png" ID="AddAlarm" runat="server" CssClass="sendsubmit" />
                        <asp:ImageButton ImageUrl="Images/add_microwave.png" ID="AddMicrovawe" runat="server" CssClass="sendsubmit" />
                        <asp:ImageButton ImageUrl="Images/add_tv.png" ID="AddTV" runat="server" CssClass="sendsubmit" />
                        <asp:ImageButton ImageUrl="Images/add_conditioner.png" ID="AddConditioner" runat="server" CssClass="sendsubmit" />
                        <br />
                    </center>
            <asp:Panel ID="panelForDevices" runat="server" ></asp:Panel>
            </div>
        </form>
        
	</body>
    
</html>
