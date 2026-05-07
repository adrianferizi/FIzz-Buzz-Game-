Imports System
Module Module1

    'Adrian Ferizi
    '07/04/26
    'Fizz Buzz Game 

    'Class stores data of players
    Public Class Player

        'private attributes 
        Private name As String
        Private lives As Integer
        Private score As Integer

        'constructor of player
        Public Sub New(ByVal playerName As String)

            name = playerName
            lives = 0
            score = 0

        End Sub

        'sets player lives 
        Public Sub setLives(ByVal Numlives As Integer)

            lives = Numlives

        End Sub

        'removes a life 
        Public Sub loseLife()

            lives = lives - 1

        End Sub

        'returns lives
        Public Function getLives() As Integer

            Return lives

        End Function

        'checks if player is eliminated
        Public Function isOut() As Boolean
            If lives <= 0 Then

                Return True

            Else

                Return False

            End If

        End Function

        'returns player names 
        Public Function getName() As String

            Return name

        End Function

        'adds 1 to score of winner

        Public Sub addScore()

            score = score + 1

        End Sub

        'display players stats 
        Public Sub displayStats()

            Console.WriteLine(name & " score: " & score)

        End Sub

    End Class


    'Game class
    Public Class Game

        'private attributes
        Private players As List(Of Player)
        Private currentNumber As Integer
        Private gameRunning As Boolean
        Private startingLives As Integer

        'constructor for games
        Public Sub New()

            players = New List(Of Player)
            currentNumber = 1
            gameRunning = False
            startingLives = 1

        End Sub

        'adds player to game
        Public Sub addPlayer(ByRef p As Player)

            players.Add(p)

        End Sub

        'sets lives for game
        Public Sub setLives(ByVal numlives As Integer)

            startingLives = numlives

        End Sub

        'displays rules of game
        Public Sub explainRules()

            Console.WriteLine("------ Fizz Buzz Rules -----")
            Console.WriteLine("Players take turns counting upwards from 1. ")
            Console.WriteLine("If the number is a multiple of 3 the player should input 'fizz' ")
            Console.WriteLine("If the number is a multiple of 5 the player should input 'buzz'")
            Console.WriteLine("If the number is a multiple of both 3 and 5 then the user should enter 'fizz buzz'")
            Console.WriteLine()

            Console.WriteLine("Wrong inputs will cause the player to lose a life until they reach 0 and are eliminated")
            Console.WriteLine("The last player remaining wins and the score is saved.")
            Console.WriteLine()
            Console.WriteLine("Example round:")
            Console.WriteLine("1
2
fizz
4
buzz
fizz
...
fizz buzz -> (15)
16
17
18 -> (incorrect input player looses a life)")

        End Sub

        'returns the correct answer
        Private Function getCorrectInputs(ByVal number As Integer) As String

            'checks if number is multiple of 3 and 5
            If number Mod 3 = 0 And number Mod 5 = 0 Then

                Return "fizz buzz"

                'checks if number is multiple of 3 
            ElseIf number Mod 3 = 0 Then

                Return "fizz"

                'checks if number is multiple of 5
            ElseIf number Mod 5 = 0 Then

                Return "buzz"
            Else

                'if not divisible by either 3 or 5 then the number should be inputted as is 
                Return number.ToString()

            End If

        End Function

        'counts remaining players
        Private Function playersRemaining() As Integer

            Dim count As Integer = 0

            For Each p In players

                If p.isOut() = False Then

                    count = count + 1

                End If
            Next
            Return count

        End Function

        'returns winners name 

        Private Function getWinner() As String

            For Each p In players
                If p.isOut() = False Then

                    Return p.getName

                End If
            Next

            Return ""
        End Function


        'this is the main loop of the game 
        Public Sub playGame()

            Dim playerInput As String
            Dim correctInput As String

            gameRunning = True

            'sets all the players lives
            For Each p In players

                p.setLives(startingLives)

            Next


            Console.Clear()


            Console.WriteLine("press any key to begin...")
            Console.ReadKey()

            While gameRunning = True

                For Each p In players

                    'skips the  eliminated players
                    If p.isOut = False Then

                        Console.WriteLine("The current number is:" & currentNumber)
                        Console.WriteLine(p.getName & "'s turn")
                        Console.WriteLine("Lives remaining: " & p.getLives)

                        correctInput = getCorrectInputs(currentNumber)

                        playerInput = Console.ReadLine

                        If playerInput.ToLower = correctInput Then

                            currentNumber = currentNumber + 1
                        Else
                            p.loseLife()
                            Console.WriteLine("INCORRECT INPUT , " & p.getName & "lost a life!")

                            If p.isOut = True Then

                                Console.WriteLine(p.getName & " has been eliminated!")

                            End If
                        End If


                        'cheks if game is over 
                        If playersRemaining() = 1 Then

                            gameRunning = False

                            Dim winnerName As String = getWinner()

                            Console.WriteLine()
                            Console.ForegroundColor = ConsoleColor.Blue
                            Console.WriteLine(winnerName & " wins!")
                            Console.ForegroundColor = ConsoleColor.White

                            'increases the winners score
                            For Each player In players
                                If player.getName = winnerName Then

                                    player.addScore()

                                End If
                            Next
                        End If
                    End If

                Next
            End While

        End Sub

    End Class


    Sub Main()

        Dim GamePlayers As New List(Of Player)
        Dim programRunning As Boolean = True

        Dim uChoice As String
        Dim numPlayInput As Integer
        Dim numLivesInput As Integer

        While programRunning = True

            Console.Clear()

            Console.WriteLine("
██████  ██  ███████  ███████      ██████  ██    ██  ███████  ███████ 
██      ██      ███      ███      ██   ██ ██    ██      ███      ███ 
█████   ██     ███      ███       ██████  ██    ██     ███      ███  
██      ██    ███      ███        ██   ██ ██    ██    ███      ███   
██      ██   ███████  ███████     ██████   ██████    ███████  ███████")



        Console.WriteLine("1. Start Game")
        Console.WriteLine("2. Display Scores")
        Console.WriteLine("3. View Rules")
        Console.WriteLine("4. Exit")

        uChoice = Console.ReadLine

            Select Case uChoice
                Case "1"
                    Dim newGame As New Game
                    Dim numPlayers As Integer
                    Dim vaildInput As Boolean = False

                    While vaildInput = False
                        Console.WriteLine("Enter the number of players: ")

                        numPlayInput = Console.ReadLine

                        If IsNumeric(numPlayInput) Then

                            numPlayers = Convert.ToInt32(numPlayInput)

                            'makes sure there is at least 2 players 
                            If numPlayers >= 2 Then

                                vaildInput = True

                            Else
                                Console.WriteLine("There must be at least 2 players")

                            End If

                        Else

                            Console.WriteLine("Invaild input")


                        End If


                    End While

                    'user inputs number of lives 
                    Dim numLives As Integer

                    vaildInput = False

                    While vaildInput = False
                        Console.WriteLine("How many lives should the players have")

                        numLivesInput = Console.ReadLine

                        If IsNumeric(numLivesInput) Then

                            numLives = Convert.ToInt32(numLivesInput)

                            If numLives > 0 Then

                                vaildInput = True
                            Else
                                Console.WriteLine("Lives must be over 0")


                            End If

                        Else

                            Console.WriteLine("Invaild input")

                        End If


                    End While

                    'sets the lives 
                    newGame.setLives(numLives)

                    'user inputs player names

                    Dim i As Integer

                    For i = 1 To numPlayers
                        Console.WriteLine("Enter name for player" & i)

                        Dim name As String = Console.ReadLine

                        Dim existingPlayer As Player = Nothing

                        'checks if the player already exists

                        For Each p In GamePlayers
                            If p.getName.ToLower = name.ToLower Then

                                existingPlayer = p
                            End If
                        Next

                        'use existing player
                        If existingPlayer IsNot Nothing Then

                            newGame.addPlayer(existingPlayer)
                        Else


                            Dim newPlayer As New Player(name)

                            GamePlayers.Add(newPlayer)
                            newGame.addPlayer(newPlayer)

                        End If


                    Next

                    newGame.playGame()

                    Console.WriteLine("Press enter to return to menu")
                    Console.ReadLine()

                'display scores
                Case "2"

                    Console.Clear()

                    Console.WriteLine("---- Player Scores ----")

                    If GamePlayers.Count = 0 Then
                        Console.WriteLine("No games have been played yet")

                    Else
                        For Each p In GamePlayers
                            p.displayStats()

                        Next
                    End If

                    Console.WriteLine("Press enter to return to menu")
                    Console.ReadLine()

                'display teh rules

                Case "3"

                    Console.Clear()
                    Dim temporaryGame As New Game
                    temporaryGame.explainRules()

                    Console.WriteLine("Press enter to return to menu")
                    Console.ReadLine()

                'exits the program 
                Case "4"

                    Console.WriteLine("Thanks for playing!")
                    programRunning = False


                Case Else

                    Console.WriteLine("Invaild input .")
                    Console.ReadLine()

            End Select

        End While

    End Sub
End Module

