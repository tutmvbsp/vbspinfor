   M S B A M L   `   `   `  ����M  ISystem, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089M3clr-namespace:System.ComponentModel;assembly=SystemSystem.ComponentModel  5   	    Presentation   Presentation.WpfUpdateSL   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   Bcm3clr-namespace:System.ComponentModel;assembly=System     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  Update Data��5   	   $	��522��6   $	��747��6*    � WindowStartupLocation$ CenterScreen=�66    � Icon$ Icons/Cryoword.ico��6[    � 	Resources 5      ,   &System.ComponentModel.BackgroundWorker   backgroundWorker   WorkerReportsProgress!   WorkerSupportsCancellation��%=   &	          5   
   -   5         .  . 5      5      .��5      � .+�5   
   �� -   5        $��btnClose��5   ]   $��Close��6   $��30��6!   $
��Left=�6-   $��583,440,0,0q�6H   $	��Top=�6m   $��81��6�   6�   �� 5   
   -   5        $��btnOk��5   ^   $��Export��6   $��30��6"   $
��Left=�6.   $��203,440,0,0q�6I   $	��Top=�6k   $��81��6�   {�.  6�   �� 5   
   -   5        $��	btnUpdate��5   ^   $��Update��6   $��30��6"   $
��Left=�6.   $��382,439,0,0q�6I   $	��Top=�6o   $��81��6�   {�.  6�   �� 5   
   -   5        $��	btnInsert��5   Z   $��OK��6   $��30��6   $
��Left=�6*   $��480,439,0,0q�6E   $	��Top=�6k   $��81��6�   6�   �� 5   
   -   5        $��btnUp��5   Z   $��Up��6   $��30��6   $
��Left=�6*   $��292,440,0,0q�6E   $	��Top=�6g   $��81��6   {�.  6�   �� 5   
   -   5        $��btnProg��5   \   $
��Prog��6   $��30��6    $
��Left=�6,   $��100,440,0,0q�6G   $	��Top=�6k   $��81��6�   {�.  6�   &   System.Windows.Controls.DataGrid  5   
   -	   5        $��dgvKu��5   i      AutoGenerateColumns . $	��175��6.   $
��Left=�6;   $��	24,48,0,0q�6V   $	��Top=�6v   $	��679��6�     5   
   -
   5        $��dgvTk��5   j    . $	��175��6.   $
��Left=�6;   $��
24,233,0,0q�6V   $	��Top=�6w   $	��679��6�   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��	dtpNgayKu��5   P   $��19��6   $
��Left=�6!   $��
109,14,0,0q�6<   $	��Top=�6a   $	��100��6y     5   
   -   5        $��	dtpNgayBt��5   Q   $��19��6   $��Right=�6!   $��
0,14,348,0q�6=   $	��Top=�6b   $	��100��6z   �� 5   
   -   5        $��	lblNgayKU��5   c   $��Ngày KU/CASA��6   $
��auto��6'   $
��Left=�65   $��	16,12,0,0q�6P   $	��Top=�6t   $
��auto��6�   �� 5   
   -   5        $��	lblNgayBT��5   _   $��Ngày BT��6   $
��auto��6"   $
��Left=�60   $��
216,12,0,0q�6K   $	��Top=�6p   $
��auto��6�   � 5   
   -   5        $��progressBar1��5   Q   $��16��6   $
��Left=�6"   $��
34,415,0,0q�6=   $	��Top=�6e   $	��666��6}   �� 5   
   -   5        $��
lblPerCent��5   [   $��Label��6   $��21��6    $
��Left=�6,   $��
38,437,0,0q�6G   $	��Top=�6m   $��88��6�   �� 5   
   -   5        $��lblLoi��5   [   $��Label��6   $��21��6    $
��Left=�6,   $��
38,458,0,0q�6G   $	��Top=�6i   $��88��6�   5      5       