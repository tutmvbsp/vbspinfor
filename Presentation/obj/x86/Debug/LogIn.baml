   M S B A M L   `   `   `  ����  Presentation    Presentation.LogIn   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$(  "Đăng nhập vào chương trình��5   	   $	��200��6,   $	��406��69    � WindowStartupLocation$ CenterScreen=�6E    � Icon$ Icons/logIn.jpg��6k   .��5      � .+�5   
   �� -   5        $��btnClose��5   ]   $��Close��6   $��26��6!   $
��Left=�6-   $��211,120,0,0q�6H   $	��Top=�6m   $��79��6�   6�   �� 5   
   -   5        $��btnOk��5   Z   $��OK��6   $��26��6   $
��Left=�6*   $��113,120,0,0q�6E   $	��Top=�6g   $��79��6   6�   �� 5   
   -   5        $��txtUserName��5   M   $��25��6   $
��Left=�6   $��
189,13,0,0q�69   $	��Top=�6`   $	��130��6x   $��0��6�   �� 5	   
   -   5        $��txtIp��5	   M   $��25��6   $
��Left=�6   $��
189,80,0,0q�69   $	��Top=�6Z   $	��130��6r   $��2��6~   �� 5
   
   -   5        $��lblUserName��5
   ^   $��UserName��6   $��25��6#   $
��Left=�6/   $��
113,16,0,0q�6J   $	��Top=�6q   $
��auto��6�   �� 5   
   -   5        $��lblPassWord��5   ^   $��PassWord��6   $��25��6#   $
��Left=�6/   $��
113,48,0,0q�6J   $	��Top=�6q   $
��auto��6�   G� 5   
   -   5        $��PassBox��5   Q   $��25��6   $
��Left=�6"   $��
189,48,0,0q�6=   $	��Top=�6`   $	��130��6x   $��1��6�   �� 5   
   $
��Left=�6   $��78��6+   $��	10,16,0,0q�67   $	��Top=�6J   $��87��6b   $��Images/KeyLogin.jpg��6m   �� 5   
   -	   5        $��lblCheck��5   W   $��Check connnect��6   $
��Left=�6)   $��	8,126,0,4q�6D    J� RenderTransformOrigin$ 0.373,0.851&�6�   $��88��6�   z�5      V� 5      .~�5      �� �� 5      �� 5       �� Angle$ 0.591Q�6&   T� 5       T� X$ -0.126Q�6)    T� Y$ -0.078Q�64   5      ��5      �� 5       �� 
TargetType" ��6    �� Triggers 5      P� 5      	 P� Property	 �@J�IsMouseOver
 P� Value
 . . �5      ��  �� Property � �� �� Value
 �  ��6C   �� 5       � ��$ Italic$�6;   �� 5       � ��$
 Bold"�6C   5      .�5!      ��  � ��
 � � �6;   �� 5"       � ��$ Times New Roman)�6;   �� 5#       �@�ToolTip$+ %Click vào đây để nhập nguồn��60   5$      5&      5(      5)      