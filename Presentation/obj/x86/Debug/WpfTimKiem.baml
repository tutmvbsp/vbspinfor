   M S B A M L   `   `   `  ����  Presentation    Presentation.WpfTimKiem   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5        $��TimKiem��5   �     � Title$.  (Nhập CMT hoặc CIF để tìm kiếm ��6	   $��238.65��60   $��620.13��6@    � WindowStartupLocation$ CenterScreen=�6O    � Icon$ /Icons/officemac01.ico��6u   .��5      � .+�5   
   (  "System.Windows.Controls.DatePicker  -   5        $��dtpNgay��5   O   $��26��6   $
��Left=�6!   $��	72,19,0,0q�6<   $	��Top=�6^   $	��110��6v   �� 5   
   -   5        $��lblNgay��5   Y   $��Ngày��6   $��30��6   $
��Left=�6+   $��	26,19,0,0q�6F   $	��Top=�6h   $��46��6�   �� 5   
   -   5        $��lblCMT��5   Y   $	��CMT��6   $��30��6   $
��Left=�6*   $��
198,19,0,0q�6E   $	��Top=�6g   $��46��6   �� 5	   
   -   5        $��lblCIF��5	   Y   $	��CIF��6   $��30��6   $
��Left=�6*   $��
367,19,0,0q�6E   $	��Top=�6g   $��46��6   �� 5
   
   -   5        $��btnClose��5
   ]   $��Close��6   $��22��6!   $
��Left=�6-   $��491,175,0,0q�6H   $	��Top=�6m   $��90��6�   6�   �� 5   
   -   5        $��btnOk��5   Z   $��OK��6   $��22��6   $
��Left=�6*   $��371,175,0,0q�6E   $	��Top=�6g   $��90��6   6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��dgvData��5      $
��Left=�6"   $��97��6=   $��	24,58,0,0q�6I   $	��Top=�6\   $	��567��6t   6�   �� 5   
   -	   5        $��txtCMT��5      $
��Left=�6    $��26��6;   $��
243,19,0,0q�6G    �� TextWrapping$
 Wrap=�6[   $�� ��6o   $	��Top=�6w   $	��114��6�   �� 5   
   -
   5        $��txtCIF��5      $
��Left=�6    $��26��6;   $��
401,20,0,0q�6G   $
 Wrap=�6[   $�� ��6o   $	��Top=�6w   $	��114��6�   �� 5   
   -   5        $��
lblTimKiem��5      $��Tìm kiếm��6E   $
��Left=�6X   $��26��6s   $��
527,19,0,0q�6   $	��Top=�6�   $��64��6�   ��5      �� 5       �� 
TargetType" ��6    �� Triggers 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5      ��  �� Property � ��	 �� Value
	 �"�"�6C   �� 5       � ��$	 Italic$�6;   �� 5       � ��$
	 Bold"�6C   5      .�5      ��  � ��
	 ��  �6;   �� 5       � ��$	 Times New Roman)�6;   �� 5       �@�ToolTip$/	 )Click vào đây để nhập số liệu��60   5      5      5      5       