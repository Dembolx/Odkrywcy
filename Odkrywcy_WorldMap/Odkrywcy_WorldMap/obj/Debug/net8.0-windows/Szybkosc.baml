   M S B A M L   `   `   `  ����  Odkrywcy_WorldMap     Odkrywcy_WorldMap.Szybkosc   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� 
Background  �#222 N� Title$ Szybki - Quiz��5       � 	Resources 5       �� 
TargetType   AnswerButtonStyle �� Property �� Value  FadeInQuestion �� TargetProperty W� From	 W� To	 g� Duration��%  &	        &	 �    �� 5   
   " ��6   .�5	      ��  � ��$ 18(�6)   �� 5
       � ��$
 Bold"�6+   �� 5       � ��
 �����6+   �� 5       � �� �#4446+   �� 5       � �� �#8886,   �� 5       � ��$ 2q�60   �� 5       � ��$	 200��6&   �� 5       � ��$ 60��6'   �� 5       � ��$ 10q�6'   �� 5       �@�Cursor$
 Hand��6'   5      �� 5   
   .��5      W� $ Opacity�6    06B    16K   $	 0:0:0.5D�6R   5      5      .��5      � 
 � RowDefinitions
 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5"   
   �� -   5         ��Name$ StartGameButton��5"      $
��Graj��6*   $��24(�5#      $
��Bold"�6   
����������#28a745$	��200��5$   &   $��80��62   $��Center=�5%      $��Center=�6.   5&      �� 5)   
   -   5         ��Name$ 	TimerText��5)      $��00:00:00 (+0s)��6'   
�������$��24(�5*   '   $
��Bold"�65   $��Right=�5+      $��20q�60   $y�Hidden=�5,      �� 5/   
   -   5        $ ErrorCounter��5/      $��Błędy: 0 (+0s)��6*   
���  ��$��20(�50   %   $
��Bold"�63   $
��Left=�51      $��20q�6/   $y�Hidden=�52      �� 55   
   -   5        $ QuestionCounter��55      $��Pytanie 1/10��6-   
�������$��20(�56   '   $
��Bold"�65   $��Center=�57      $��10q�61   $y�Hidden=�58      �� 5;   
   -   5        $ QuestionText��5;      $��
Pytanie...��6*   
�������$��22(�5<   '   $
��Bold"�65   $��Center=�5=      $��Center=�61    �� TextAlignment$ Center=�5>       J� Opacity$ 0Q�6+   $y�Hidden=�5?       � Triggers 5@      :� 5A       :� RoutedEvent$ Loaded��6   .[�5B      �� #��[ 6&   5C      5E      �� 5H   
   -   5         ��Name$ AnswerButtons��5H      $��1��6,    �� Orientation$ Vertical=�5I      $��Center=�6,   $��Center=�5J      $y�Hidden=�60   �� 5M   
   -   5        $��Wyjdź��5M      #��[  5N      $
��Left=�5O      $��Bottom=�6,   $��20q�5P      5Q      5R      