   M S B A M L   `   `   `  ����  Presentation    Presentation.WpfTTKH   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   k gTelerik.Windows.Documents.Core, Version=2018.2.619.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7o kTelerik.Windows.Controls.GridView, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7b	 ^Telerik.Windows.Data, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7l
 hTelerik.Windows.Controls.Input, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f bTelerik.Windows.Controls, Version=2012.3.1017.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7z vTelerik.Windows.Controls.FixedDocumentViewers, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2018.2.620.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Mtelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
    5   	   -   5          � Title$  Thông tin khách hàng��5   	   $��151.272��6&   $��396.371��67    � WindowStartupLocation$  CenterScreen=�6G    � Icon$ Icons/My Video.ico��6n   .��5      � $��0,0,0,-1q�6   .+�5   
   (  "System.Windows.Controls.DatePicker  -   5          Name$ dtpNgay��5   O   $��24��6   $
��Left=�6!   $��	58,17,0,0q�6<   $	��Top=�6`   $	��111��6x   �� 5   
   -   5         ��Name$ lblNgay��5   X   $��Ngày��6   $��26��6   $
��Left=�6+   $��6,17,0,0q�6F   $	��Top=�6i   $��42��6�   �� 5	   
   -   5        $ lblMaKH��5	   [   $��Mã KH��6   $��26��6    $
��Left=�6,   $��
188,17,0,0q�6G   $	��Top=�6l   $��56��6�   �� 5
   
   -   5         ��Name$ txtMakh��5
   a   $
��Left=�6   $��19��6-   $��
244,20,0,0q�69    �� TextWrapping$
 Wrap=�6M   $�� ��6r   $	��Top=�6z   $	��116��6�   �� 5   
   -   5        $	��Xem��5      $
��Left=�6>   $
��auto��6Y   $��
107,83,0,0q�6g   $	��Top=�6{   $
��auto��6�   ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � ��	 �� Value
	 � � �63   �� 5       � ��$
	 Bold"�63   �� 5       � ��$	 Times New Roman)�63   �� 5       �@�ToolTip$/	 )Click vào đây để nhập số liệu��60   
 �� Triggers
 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5      ��  � ��
	 �  ��6;   �� 5       � ��$	 Italic$�6;   �� 5       � ��$
	 Bold"�6;   5      5      5      �� 5   
   -   5        $��Thoát��5      $
��Left=�6A   $
��auto��6\   $��
213,83,0,0q�6j   $	��Top=�6~   $
��auto��6�    J� RenderTransformOrigin$ -1.738,0.448&�6�   ��5      �� 5      " ��6   .�5      ��  � ��
	 �  ��63   �� 5        � ��$
	 Bold"�63   �� 5!       � ��$	 Times New Roman)�63   �� 5"       �@�ToolTip$/	 )Click vào đây để nhập số liệu��60   
 5#      P� 5$       �@J�IsMouseOver . . �5%      ��  � ��
	 � � �6;   �� 5&       � ��$	 Italic$�6;   �� 5'       � ��$
	 Bold"�6;   5(      5*      5,      �� 5-   
   -   5        $ 
lblTimKiem��5-   h   $��Tìm kiếm theo tên��6   $
��auto��6,   $
��Left=�6:   $��	10,47,0,0q�6U   $	��Top=�6|   $
��auto��6�   �� 5.   
   -	   5        $ txtTenKh��5.   a   $
��Left=�6   $��19��6-   $��
123,48,0,0q�69   $
 Wrap=�6M   $�� ��6s   $	��Top=�6{   $	��116��6�   6�   * 
 $Telerik.Windows.Controls.RadComboBox  5/   
   -
   5          Name$ CboTenKh��5/      $
��Left=�60   $��
244,47,0,0q�6K   $	��Top=�6_   $	��116��6w   6�   50      51      