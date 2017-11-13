''' <summary>
''' 碎片拼接器
''' </summary>
Public Class FragmentSplicer

    ''' <summary>
    ''' 拼接当前容器内所有碎片
    ''' </summary>
    Public Sub MosaickImage2D(Fragments As List(Of Fragment))
        Dim IndexList As New List(Of Integer)
        Dim fragmentFinished As New FragmentContainer(209, 209)
        Dim fragmentUnfinished As New FragmentContainer(209, 209)
        Dim fragmentUnfinishedTemp As New FragmentContainer(209, 209)

        '预置第一个碎片
        fragmentFinished.Add(Fragments(0), 0, 0)
        fragmentUnfinishedTemp.Fragments.RemoveAt(0)

        While fragmentFinished.Fragments.Count < fragmentUnfinished.Fragments.Count
            fragmentFinished.Map.CalcCell(fragmentFinished.Fragments)
            GetBorderValue(fragmentFinished, fragmentUnfinished)
            Dim tempIndex As Integer = GetMaxBorderValue(fragmentFinished.Map)
            If fragmentFinished.Map.Value(CInt(fragmentFinished.Map.Cells(tempIndex).Location.X), CInt(fragmentFinished.Map.Cells(tempIndex).Location.Y)) Then
                fragmentFinished.MoveTo(fragmentUnfinishedTemp, CInt(fragmentFinished.Map.Cells(tempIndex).Location.X), CInt(fragmentFinished.Map.Cells(tempIndex).Location.Y))
            End If

            IndexList.Insert(0, fragmentFinished.Fragments.IndexOf(fragmentFinished.Map.Cells(tempIndex).Fragment))
            If IndexList.Count > 29 Then IndexList.Remove(29)
            If IsLoop(IndexList) Then
                fragmentFinished.Map.Cells(tempIndex).Fragment = fragmentUnfinishedTemp.Fragments(0)
            End If

            If fragmentFinished.Fragments.Contains(fragmentFinished.Map.Cells(tempIndex).Fragment) Then
                fragmentFinished.Remove(fragmentFinished.Map.Cells(tempIndex).Fragment, CInt(fragmentFinished.Map.Cells(tempIndex).Location.X), CInt(fragmentFinished.Map.Cells(tempIndex).Location.Y))
            Else


            End If
            fragmentFinished.Add(fragmentFinished.Map.Cells(tempIndex).Fragment, CInt(fragmentFinished.Map.Cells(tempIndex).Location.X), CInt(fragmentFinished.Map.Cells(tempIndex).Location.Y))
            fragmentUnfinishedTemp.Fragments.Remove(fragmentFinished.Map.Cells(tempIndex).Fragment)

            If fragmentFinished.Fragments.Count Mod 5 = 0 Then
                Dim minX As Single = 1000
                Dim minY As Single = 1000
                Dim MaxX As Single = -1000
                Dim MaxY As Single = -1000
                For Each subborder As Fragment In fragmentFinished.Fragments
                    If minX > subborder.Location.X Then
                        minX = subborder.Location.X
                    End If
                    If MaxX < subborder.Location.X Then
                        MaxX = subborder.Location.X
                    End If
                    If minY > subborder.Location.Y Then
                        minY = subborder.Location.Y
                    End If
                    If MaxY < subborder.Location.Y Then
                        MaxY = subborder.Location.Y
                    End If
                Next

                'Dim AllImage As New Bitmap((MaxX - minX + 1) * ImageWidth, (MaxY - minY + 1) * ImageHeight)
                'Using pg = Graphics.FromImage(AllImage)
                '    Dim tmpStr As String = ""
                '    For i = 0 To fragmentFinished.Fragments.Count - 1
                '        tmpStr = tmpStr & "[" & fragmentFinished.Fragments(i).Location.X & "," & fragmentFinished.Fragments(i).Location.X & "] "
                '    Next
                '    ' MsgBox(tmpStr)
                '    For i = 0 To fragmentFinished.Fragments.Count - 1
                '        Dim x As Integer = ImageWidth * (fragmentFinished.Fragments(i).Location.X - minX)
                '        Dim y As Integer = ImageHeight * (fragmentFinished.Fragments(i).Location.Y - minY)
                '        pg.DrawImage(fragmentFinished.Fragments(i).Image, x, y, ImageWidth, ImageHeight)
                '    Next
                'End Using
                'PicBox.Image = AllImage
                'PicBox.Refresh()
                'If fragmentFinished.Fragments.Count < fragmentUnfinished.ScripList.Count Then AllImage.Dispose()
            End If
        End While
    End Sub

    ''' <summary>
    ''' 获取指定已完成碎片的Map中Border的值
    ''' </summary>
    ''' <param name="CScrip">已完成的碎片</param>
    ''' <param name="UScrip">未完成的碎片</param>
    Private Sub GetBorderValue(ByRef CScrip As FragmentContainer, ByRef UScrip As FragmentContainer)
        For Each SubBorder In CScrip.Map.Cells
            Dim CompareData(UScrip.Fragments.Count - 1) As Integer
            Dim tempData(UScrip.Fragments.Count - 1) As Integer
            Dim CurrentScrip As Fragment = Nothing
            For i = 0 To 3
                If SubBorder.Around.FourB(i) Then
                    For Each SubScrip In CScrip.Fragments
                        If SubScrip.Location = SubBorder.Around.FourPoint(i) Then
                            CurrentScrip = SubScrip
                            Exit For
                        End If
                    Next
                    If Not CurrentScrip Is Nothing Then tempData = GetComparedValue(CurrentScrip, UScrip.Fragments, i)
                    For j = 0 To UScrip.Fragments.Count - 1
                        CompareData(j) += tempData(j)
                    Next
                End If
            Next
            Dim tempIndex As Integer = GetMaxIndex(CompareData)

            If SubBorder.Fragment Is UScrip.Fragments(tempIndex) Then '自身
                SubBorder.Value = -10000000
            Else
                SubBorder.Value = CompareData(tempIndex)
            End If
            SubBorder.Fragment = UScrip.Fragments(tempIndex)

        Next
    End Sub
    ''' <summary>
    ''' 返回指定碎片与List中所有碎片的匹配度
    ''' </summary>
    ''' <param name="CurrentScrip">指定的碎片</param>
    ''' <param name="gScripList">指定的List</param>
    ''' <param name="Type">比较类型</param>
    ''' <returns></returns>
    Private Function GetComparedValue(ByRef CurrentScrip As Fragment, ByRef gScripList As List(Of Fragment), Type As Integer) As Integer()
        Dim CompareData(gScripList.Count - 1) As Integer '比较结果，反应两个序列的匹配度
        Dim cArr As Integer() = {3, 2, 1, 0}
        Dim gArr As Integer() = {2, 3, 0, 1}
        For i = 0 To gScripList.Count - 1
            If Not CurrentScrip Is gScripList(i) Then
                CompareData(i) += CompareInteger(CurrentScrip.Border.Borders(cArr(Type)).Threshold, gScripList(i).Border.Borders(gArr(Type)).Threshold)
            End If
        Next
        Return CompareData
    End Function
    ''' <summary>
    ''' 返回指定MapClass的orderList中的元素Value属性最大值的索引
    ''' </summary>
    ''' <param name="gMap">指定的MapClass</param>
    ''' <returns></returns>
    Private Function GetMaxBorderValue(gMap As FragmentContainerMap) As Integer
        Dim tempIndex As Integer = 0
        Dim tempData As Integer = -1
        For i = 0 To gMap.Cells.Count - 1
            If gMap.Cells(i).Value > tempData Then
                tempData = gMap.Cells(i).Value
                tempIndex = i
            End If
        Next
        Return tempIndex
    End Function
    ''' <summary>
    ''' 返回指定的两个数组的相似度
    ''' </summary>
    ''' <param name="LeftInt"></param>
    ''' <param name="RightInt"></param>
    ''' <returns></returns>
    Private Function CompareInteger(LeftInt As Integer(), RightInt As Integer()) As Integer
        Dim tempInt As Integer
        For i = 0 To LeftInt.Count - 1
            If LeftInt(i) = 1 AndAlso RightInt(i) = 1 Then
                tempInt += 5
            ElseIf LeftInt(i) = 0 AndAlso RightInt(i) = 1 Then
                tempInt += 0
            ElseIf LeftInt(i) = 1 AndAlso RightInt(i) = 0 Then
                tempInt += 0
            ElseIf LeftInt(i) = 0 AndAlso RightInt(i) = 0 Then
                tempInt += 1
            End If
        Next
        Return tempInt
    End Function
    ''' <summary>
    ''' 返回指定的两个List序列的相似度
    ''' </summary>
    ''' <param name="leftList"></param>
    ''' <param name="RightList"></param>
    ''' <returns></returns>
    Private Function CompareList(leftList As List(Of Integer), RightList As List(Of Integer)) As Double
        Dim tempDouble As Double = 0
        For Each SubInteger As Integer In leftList
            For Each SubInteger2 As Integer In RightList
                If SubInteger = SubInteger2 Then tempDouble += 1
            Next
        Next
        Return tempDouble / （(leftList.Count + RightList.Count) / 2)
    End Function
    Private Function CompareMargin(LeftMargin As Integer, RightMargin As Integer) As Double
        Dim TempDouble As Double
        If LeftMargin = 0 And RightMargin = 0 Then Return 1
        TempDouble = 1 - Math.Abs(LeftMargin + RightMargin - 42) / 42
        Return TempDouble
    End Function
    Private Function CompareMarginList(LeftMargin As List(Of Integer), RightMargin As List(Of Integer)) As Double
        Dim TempDouble As Double
        Dim Count As Integer = Math.Min(LeftMargin.Count, RightMargin.Count)
        If Count > 0 Then
            For i = 0 To Count - 1
                TempDouble += 1 - Math.Abs(LeftMargin(i) + RightMargin(i) - 42) / 42
            Next
        End If
        ' If LeftMargin = 0 And RightMargin = 0 Then Return 1
        ' TempDouble = 1 - Math.Abs(LeftMargin + RightMargin - 42) / 42

        Return 1 + TempDouble / Count
    End Function
    ''' <summary>
    ''' 返回指定的数组中最大值的索引
    ''' </summary>
    ''' <param name="CompareData">指定的数组</param>
    ''' <returns></returns>
    Private Function GetMaxIndex(CompareData() As Integer) As Integer
        Dim tempIndex As Integer = 0
        Dim tempData As Integer = -1
        For i = 0 To CompareData.Count - 1
            If CompareData(i) > tempData Then
                tempData = CompareData(i)
                tempIndex = i
            End If
        Next
        Return tempIndex
    End Function

    Private Function IsLoop(IndexList As List(Of Integer)) As Boolean
        Dim bLoop As Boolean
        If IndexList.Count >= 6 Then
            For i = 2 To IndexList.Count \ 3
                bLoop = True
                For j = 0 To i - 1
                    If Not (IndexList(j) = IndexList(i + j) And IndexList(i + j) = IndexList(2 * i + j) And IndexList(j) >= 0) Then
                        bLoop = False
                        Exit For
                    End If
                Next
                If bLoop = True Then Return True
            Next
        End If
        Return False
    End Function
End Class
