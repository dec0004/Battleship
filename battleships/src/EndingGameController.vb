Imports SwinGameSDK

''' <summary>
''' The EndingGameController is responsible for managing the interactions at the end
''' of a game.
''' </summary>

Module EndingGameController

    ''' <summary>
    ''' Draw the end of the game screen, shows the win/lose state
    ''' </summary>
    Public Sub DrawEndOfGame()
	
		Dim toDraw as Rectangle
		Dim whatShouldIPrint as String
		
        DrawField(ComputerPlayer.PlayerGrid, ComputerPlayer, True)
        DrawSmallField(HumanPlayer.PlayerGrid, HumanPlayer)

		toDraw.X = 0
		toDraw.Y = 250
		toDraw.Width = SwinGame.ScreenWidth ()
		toDraw.Height = SwinGame.ScreenHeight ()
        
		If HumanPlayer.IsDestroyed Then
            whatShouldIPrint = "YOU LOSE!"
        Else
            whatShouldIPrint = "-- WINNER --"
        End If
		
		SwinGame.DrawTextLines(whatShouldIPrint, Color.White, Color.Transparent, GameResources.GameFont ("ArialLarge"), FontAlignment.AlignCenter, toDraw)
		
        
    End Sub

    ''' <summary>
    ''' Handle the input during the end of the game. Any interaction
    ''' will result in it reading in the highsSwinGame.
    ''' </summary>
    Public Sub HandleEndOfGameInput()
        If SwinGame.MouseClicked(MouseButton.LeftButton) _
            OrElse SwinGame.KeyTyped(KeyCode.VK_RETURN) _
            OrElse SwinGame.KeyTyped(KeyCode.VK_ESCAPE) Then
            ReadHighScore(HumanPlayer.Score)
            EndCurrentState()
        End If
    End Sub

End Module
