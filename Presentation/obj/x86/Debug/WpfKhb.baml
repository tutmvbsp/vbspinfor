   M S B A M L   `   `   `  ����  Presentation    Presentation.WpfKhb   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   k gTelerik.Windows.Documents.Core, Version=2018.2.619.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.GridView, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7b	 ^Telerik.Windows.Data, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l
 hTelerik.Windows.Controls.Input, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f bTelerik.Windows.Controls, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7z vTelerik.Windows.Controls.FixedDocumentViewers, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Mtelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
    5   	   -   5          � Title$  Phân bổ lãi KHB��5   	   $	��208��6!   $	��398��6.    � WindowStartupLocation$ CenterScreen=�6:    � Icon$ Icons/GIF-Image.ico��6_   .��5      � .+�5   
   (  "System.Windows.Controls.DatePicker  -   5          Name$ dtpNgay��5   P   $��26��6   $
��Left=�6!   $��
133,68,0,0q�6<   $	��Top=�6a   $	��139��6y   �� 5   
   -   5         ��Name$ 	lblManual��5   X   $��Thêm dữ liệu��6   $
��Left=�6'   $��290,18,0,125q�6B    J� RenderTransformOrigin$ 0.5,0.5&�6�   $��80��6�   z�5	      V� 5
      .~�5      �� �� 5      �� 5       �� Angle$ 0.591Q�6&   T� 5      5      ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � ��	 �� Value
	 � � �63   �� 5       � ��$	 Times New Roman)�63   �� 5       �@�ToolTip$/	 )Click vào đây để nhập số liệu��60   
 �� Triggers
 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5      ��  � ��
	 �  ��6;   �� 5       � ��$	 Italic$�6;   �� 5       � ��$
	 Bold"�6;   5      5      5      �� 5"   
   -   5        $��75��5"      $��102,123,198,22q�6   $��OK��6S    � FocusVisualStyle 67   Z� ��5#      �� 5$      " ��6   .�5%      ��  � ��	 5&      �� 5'       �� 
TargetType" ��6.   .R�5(   "   ��  ��Name$ border��6)   $��1q�69    �� Padding$	 4,2q�6M   
������� �� CornerRadius$ 3��6r   #��z��6�   ._�5)   &   ��  ��Name$ content��6o   $��Center=�67   $��Center=�6T   6o   5*   #   5+      5-      
 5.      P� 5/       �@J�IsMouseOver . . �50      ��  � ��
	 �/���6;   51      53      55      �� 57   
   -   5        $��75��57      $��222,123,76,22q�6   $��Close��6R    66   Z� ��58      �� 59      " ��6   .�5:      ��  � ��	 5;      �� 5<      " ��6.   .R�5=   "   �� $ border��6)   $��1q�69   $	 4,2q�6M   
�������$ 3��6r   #��z��6�   ._�5>   &   �� $ content��6o   $��Center=�67   $��Center=�6T   6o   5?   #   5@      5B      
 5C      P� 5D       �@J�IsMouseOver . . �5E      ��  � ��
	 �/���6;   5F      5H      5J      �� 5K   
   -   5        $ label1��5K   Y   $��Ngày��6   $��26��6   $
��Left=�6+   $��	64,67,0,0q�6F   $	��Top=�6i   $
��auto��6�   ���	#FF1393136�   �� 5L   
   -   5         ��Name$ cboPos��5L   N   $��22��6   $
��Left=�6   $��
133,18,0,0q�6:   $	��Top=�6^   $	��139��6v   �� 5M   
   -   5        $ lblPos��5M   X   $	��POS��6   $��23��6   $
��Left=�6*   $��	64,22,0,0q�6E   $	��Top=�6h   $��60��6�   ���	#FF1393136�   5O      5P      