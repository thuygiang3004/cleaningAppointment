   M S B A M L   `   `   `  ????W  SHouseCleaningThuyGiangNguyen, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:HouseCleaningThuyGiangNguyenHouseCleaningThuyGiangNguyen  5   	   :    4HouseCleaningThuyGiangNguyen.CustomerAppointmentList   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:HouseCleaningThuyGiangNguyen     5   	     ? Title$  Customer Appointment List??5	   	   $	??600??6+   $
??1000??68    ? 	Resources 5
         RightAligned ?? 
TargetType ?? Property ?? Value??%?   &	        ?? 5   
   " ??6%   .?5      ??  ? ??$ Right=?64   ?? 5       ?@??Padding$ 0 0 10 0q?6(   5      5      .??5      ?  ? ColumnDefinitions 5   
   ?? 5      ?? 5      ?? 5      ?? 5       ? RowDefinitions 5   
   ?? 5      ?? 5      $??5* ?6   ?? 5      .+?5   
   ?? -   5        $??OptionsLabel??5      $??0??6$   $??0??61   $??5q?6A   $??20(?6L   $??Center=?5      .??62   	Options6;   ?? 5   
   -   5        $??Options??5      $??0??6"   $??1??6/   $??5q?6?   $??20(?6J   $??Center=?5       ?? DisplayMemberPath$ Description??5       ??5   2   ?? *CleanerTypeListWithAll+ ?? SelectedValue 5!      ?? *SelectedCleanerType+5"      &   System.Windows.Controls.DataGrid  5$   
   -   5        $??	Customers??5$      $??1??6$   $??0??61   $??4??6A   $??5q?6U   $??15(?6`   	   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$	 * 5%      $??Center=?6#   
   CanUserSortColumns
 .    
IsReadOnly .    AutoGenerateColumns .     CanUserAddRows .  ??5'      ?? *CustomerAppointmentList+ ?? SelectedItem 6C   ?? *SelectedCustomerAppointment+   Columns 5(      0  *System.Windows.Controls.DataGridTextColumn  5)      -   5        Name??,  &System.Windows.Controls.DataGridColumn   Header$ Customer Name??5)   3      SortMemberPath$
 Name??6J   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6a   ??  ?? Path$
 Name?  5*      -   5        
PostalCode??$ Postal Code??5*   9   $ 
PostalCode??6N    6j   ?? $ 
PostalCode?  5+      -   5        	Phone??$ Phone??5+   4   $ Phone??6C    6Z   ?? $ Phone?  5,      -   5        ServiceDate??$ Service Date??5,   :   $ ServiceDate??6P      Width$ 2* 6?    6m   ?? $ ServiceDate?6?     5-      -   5        	HouseSize??$ 
House Size??5-   8   $ 	HouseSize??6L      ElementStyle# [  6?    6g   ?? $ 	HouseSize? ?? StringFormat$ N2??6?     5.      -	   5        ThisCleanerType??$ Cleaner Type??5.   >   $ CleanerType??6T    6q   ?? $! ThisCleanerType.Description?  5/      -
   5        Cost??$
 Cost??5/   3   $
 Cost??6A   # [  6?    6W   ?? $
 Cost?$ N2??6?   51      ?? 53   
   -   5        $
??Edit??53      $??2??6   $??2??6*   $??5q?6:   $??20(?6E   $??Center=?54      $??Center=?62   .??55   $   Edit6*   ?? 56   
   -   5        $??Delete??56      $??2??6   $??3??6,   $??5q?6<   $??20(?6G   $??Center=?57      $??Center=?62   .??58   &   Delete6.   59      5:      