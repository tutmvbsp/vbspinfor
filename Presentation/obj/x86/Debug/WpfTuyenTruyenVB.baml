   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	    Presentation#   Presentation.WpfTuyenTruyenVB   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   k gTelerik.Windows.Documents.Core, Version=2018.2.619.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o	 kTelerik.Windows.Controls.GridView, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7b
 ^Telerik.Windows.Data, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l hTelerik.Windows.Controls.Input, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f bTelerik.Windows.Controls, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7z vTelerik.Windows.Controls.FixedDocumentViewers, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Mtelerik1http://schemas.telerik.com/2008/xaml/presentation   	 
     5   	   -   5          � Title$$  Nhập văn bản chỉ đạo��5   	   $��276.818��6&   $��725.904��67    � WindowStartupLocation$ CenterScreen=�6G    � Icon$ Icons/VBSP.ico��6l    � 
ResizeMode$ CanMinimize=�6�    � WindowStyle$ ThreeDBorderWindow=�6�   .��5      � $��0,-1,-0.2,11.6q�6   .+�5	   
   �� -   5         ��Name$ OK��5	   ~   $
��Lưu��6   $
��Left=�6   $��24��6:   $��264,199,0,0q�6F   $	��Top=�6[   $��84��6s   ��5
      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value 5      �� 5      	 �� 
TargetType"	 ��6.   .R�5   "   �� 
 ��Name$
 border��6)   $��1q�69    �� Padding$	 4,2q�6M   
������� �� CornerRadius$ 3��6r   #��z��6�   ._�5   &   ��  ��Name$ content��6o   $��Center=�67   $��Center=�6T   6o   5   #   5      5       �� Triggers 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5      ��  � ��
 �/���6;   5      5      5      �� 5   
   -   5        $ btnClose��5      $��Đóng��6   $
��Left=�6    $��24��6;   $��381,199,0,0q�6G   $	��Top=�6\   $��84��6t   ��5      �� 5       " ��6   .�5!      ��  � �� 5"      �� 5#      "	 ��6.   .R�5$   "   �� $
 border��6)   $��1q�69   $	 4,2q�6M   
�������$ 3��6r   #��z��6�   ._�5%   &   �� $ content��6o   $��Center=�67   $��Center=�6T   6o   5&   #   5'      5)       5*      P� 5+       �@J�IsMouseOver . . �5,      ��  � ��
 �/���6;   5-      5/      52      �� 53   
   -   5         ��Name$ lblXa��53   `   $��Người ký��6   $
��auto��6#   $
��Left=�61   $��
343,19,0,0q�6L   $	��Top=�6o   $
��auto��6�   �� 54   
   -   5        $ lblPos��54   [   $	��POS��6   $
��auto��6   $
��Left=�6,   $��
190,19,0,0q�6G   $	��Top=�6k   $
��auto��6�   �� 55   
   -   5        $ lblChDe��55   _   $��Chức vụ��6   $
��auto��6"   $
��Left=�60   $��
522,17,0,0q�6K   $	��Top=�6p   $
��auto��6�   *  $Telerik.Windows.Controls.RadComboBox  56   
   -   5          Name$ 	RadCboPos��56      $
��Left=�61   $��
227,19,0,0q�6L   $	��Top=�6`   $	��110��6x   6�     57   
   -   5        $ RadCboKy��57      $
��Left=�60   $��
402,19,0,0q�6K   $	��Top=�6_   $	��110��6w   6�     58   
   -	   5        $ 	RadCboCvu��58      $
��Left=�61   $��
577,18,0,0q�6L   $	��Top=�6`   $	��110��6x   6�   (  "System.Windows.Controls.DatePicker  59   
   -
   5          Name$ dtpNgay��59   O   $��26��6   $
��Left=�6!   $��	61,15,0,0q�6<   $	��Top=�6`   $	��110��6x     5:   
   -   5        $ 	dtpNgayKy��5:   P   $��26��6   $
��Left=�6!   $��
550,66,0,0q�6<   $	��Top=�6c   $	��110��6{   �� 5;   
   -   5        $ lblNgay��5;   Y   $��Ngày��6   $��30��6   $
��Left=�6+   $��	18,15,0,0q�6F   $	��Top=�6j   $��46��6�    J� RenderTransformOrigin$ -2.191,-0.167&�6�   �� 5<   
   -   5        $ lblSoVB��5<   a   $��Số văn bản��6   $
��auto��6%   $
��Left=�63   $��	80,66,0,0q�6N   $	��Top=�6r   $
��auto��6�   �� 5=   
   -   5        $ 	lblNgayKy��5=   _   $��	Ngày ký��6   $
��auto��6"   $
��Left=�60   $��
496,67,0,0q�6K   $	��Top=�6r   $
��auto��6�   �� 5>   
   -   5        $ 	lblGhiChu��5>   `   $��
Nội dung��6   $
��auto��6#   $
��Left=�61   $��
80,130,0,0q�6L   $	��Top=�6t   $
��auto��6�   �� 5?   
   -   5         ��Name$ txtSoVB��5?      $
��Left=�6#   $��26��6>   $��
149,65,0,0q�6J    �� TextWrapping$
 Wrap=�6^   $�� ��6r   $	��Top=�6z   $	��342��6�   �� 5@   
   -   5        $ 
txtNoiDung��5@      $
��Left=�6&   $��86��6A   $��
149,99,0,0q�6M   $
 Wrap=�6a   $�� ��6u   $	��Top=�6}   $	��538��6�   5A      5B      