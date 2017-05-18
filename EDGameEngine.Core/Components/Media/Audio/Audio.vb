Imports SharpDX.Multimedia
Imports SharpDX.XAudio2
''' <summary>
''' 音效器
''' </summary>
Public Class Audio
    Implements IAudio, IDisposable

    Public Property ComponentType As ComponentType = ComponentType.Audio Implements IGameComponent.ComponentType
    Public Property Target As IGameVisual Implements IGameComponent.Target

    ''' <summary>
    ''' 声音文件路径
    ''' </summary>
    ''' <returns></returns>
    Public Property AudioFileName As String
    ''' <summary>
    ''' XAudio2设备
    ''' </summary>
    Public Shared Property Device As XAudio2
    ''' <summary>
    ''' 声音
    ''' </summary>
    Public Property Voice As SourceVoice
    ''' <summary>
    ''' 音频格式
    ''' </summary>
    Private CurrentFormat As WaveFormat
    ''' <summary>
    ''' 音频缓冲
    ''' </summary>
    Private CurrentBuffer As AudioBuffer
    ''' <summary>
    ''' 编码信息
    ''' </summary>
    Private PacketsInfo As UInteger()

    Public Sub New()
        If Device Is Nothing Then
            Device = New XAudio2()
            Device.StartEngine()
            Dim mv As New MasteringVoice(Device)
        End If
    End Sub

    Public Sub Play() Implements IMedia.Play
        Play(1)
    End Sub
    Public Sub Play(Optional volume As Single = 1.0F)
        Voice.Stop()
        LoadBuffer()
        Voice?.SetVolume(volume)
        Voice?.Start(0)
    End Sub
    Public Sub Pause() Implements IMedia.Pause
        Throw New NotImplementedException()
    End Sub

    Public Sub [Stop]() Implements IMedia.Stop
        Voice?.Stop()
    End Sub

    Public Async Sub Start() Implements IGameObject.Start
        Await LoadFile(AudioFileName)
    End Sub
    Public Sub Update() Implements IGameObject.Update

    End Sub
    ''' <summary>
    ''' 加载音频文件
    ''' </summary>
    Public Async Function LoadFile(fileName As String) As Task(Of Boolean)
        Try
            Voice = Await CreateVoiceFromFile(Device, fileName)
            'LoadBuffer()
            Return True
        Catch
            Return False
        End Try
    End Function
    ''' <summary>
    ''' 从文件创建SourceVoice对象
    ''' </summary>
    Protected Async Function CreateVoiceFromFile(device As XAudio2, fileName As String) As Task(Of SourceVoice)
        Dim file = Await Package.Current.InstalledLocation.GetFileAsync(fileName)
        Dim streamWithContentType = Await file.OpenReadAsync()
        Dim st = streamWithContentType.AsStreamForRead()
        Using stream = New SoundStream(st)
            CurrentFormat = stream.Format
            CurrentBuffer = New AudioBuffer() With {
            .Stream = stream.ToDataStream(),
            .AudioBytes = CInt(stream.Length),
            .Flags = BufferFlags.EndOfStream
        }
            PacketsInfo = stream.DecodedPacketsInfo
        End Using
        Dim sourceVoice = New SourceVoice(device, CurrentFormat, True)
        Return sourceVoice
    End Function
    ''' <summary>
    ''' 重新加载缓冲
    ''' </summary>
    Protected Sub LoadBuffer()
        Voice?.FlushSourceBuffers()
        Voice?.SubmitSourceBuffer(CurrentBuffer, PacketsInfo)
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 要检测冗余调用

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If Not disposing Then
                ' TODO: 释放托管状态(托管对象)。
                Return
            End If
            Voice.DestroyVoice()
            Voice.Dispose()
            CurrentBuffer.Stream.Dispose()
            ' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
            ' TODO: 将大型字段设置为 null。
        End If
        disposedValue = True
    End Sub

    ' TODO: 仅当以上 Dispose(disposing As Boolean)拥有用于释放未托管资源的代码时才替代 Finalize()。
    Protected Overrides Sub Finalize()
        ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
        Dispose(False)
        MyBase.Finalize()
    End Sub

    ' Visual Basic 添加此代码以正确实现可释放模式。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
        Dispose(True)
        ' TODO: 如果在以上内容中替代了 Finalize()，则取消注释以下行。
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
