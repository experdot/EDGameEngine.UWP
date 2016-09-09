Imports System
Imports System.Diagnostics
Imports System.Runtime.CompilerServices

Namespace Global.Box2D
    Friend Class ContactManager
        
        ' Methods
        Friend Sub New()
            Me._addPair = New Action(Of Fixture, Fixture)(AddressOf Me.AddPair)
            Me._contactList = Nothing
            Me._contactCount = 0
            Me.ContactFilter = New DefaultContactFilter
            Me.ContactListener = New DefaultContactListener
        End Sub

        Friend Sub AddPair(ByVal proxyUserDataA As Fixture, ByVal proxyUserDataB As Fixture)
            Dim fixtureA As Fixture = proxyUserDataA
            Dim fixtureB As Fixture = proxyUserDataB
            Dim other As Body = fixtureA.GetBody
            Dim body As Body = fixtureB.GetBody
            If ((Not other Is body) AndAlso (Not other.IsStatic OrElse Not body.IsStatic)) Then
                Dim edge As ContactEdge = body.ConactList
                Do While (edge IsNot Nothing)
                    If (edge.Other Is other) Then
                        Dim fixture3 As Fixture = edge.Contact.GetFixtureA
                        Dim fixture4 As Fixture = edge.Contact.GetFixtureB
                        If (((fixture3 Is fixtureA) AndAlso (fixture4 Is fixtureB)) OrElse ((fixture3 Is fixtureB) AndAlso (fixture4 Is fixtureA))) Then
                            Return
                        End If
                    End If
                    edge = edge.Next
                Loop
                If (Not body.IsConnected(other) AndAlso Me.ContactFilter.ShouldCollide(fixtureA, fixtureB)) Then
                    Dim contact As Contact = Contact.Create(fixtureA, fixtureB)
                    fixtureA = contact.GetFixtureA
                    fixtureB = contact.GetFixtureB
                    other = fixtureA.GetBody
                    body = fixtureB.GetBody
                    contact._prev = Nothing
                    contact._next = Me._contactList
                    If (Me._contactList IsNot Nothing) Then
                        Me._contactList._prev = contact
                    End If
                    Me._contactList = contact
                    contact._nodeA.Contact = contact
                    contact._nodeA.Other = body
                    contact._nodeA.Prev = Nothing
                    contact._nodeA.Next = other._contactList
                    If (other._contactList IsNot Nothing) Then
                        other._contactList.Prev = contact._nodeA
                    End If
                    other._contactList = contact._nodeA
                    contact._nodeB.Contact = contact
                    contact._nodeB.Other = other
                    contact._nodeB.Prev = Nothing
                    contact._nodeB.Next = body._contactList
                    If (body._contactList IsNot Nothing) Then
                        body._contactList.Prev = contact._nodeB
                    End If
                    body._contactList = contact._nodeB
                    Me._contactCount += 1
                End If
            End If
        End Sub

        Friend Sub Collide()
            Dim [next] As Contact = Me._contactList
            Do While ([next] IsNot Nothing)
                Dim fixtureA As Fixture = [next].GetFixtureA
                Dim fixtureB As Fixture = [next].GetFixtureB
                Dim other As Body = fixtureA.GetBody
                Dim body As Body = fixtureB.GetBody
                If (other.IsSleeping AndAlso body.IsSleeping) Then
                    [next] = [next].GetNext
                Else
                    Dim aabb As AABB
                    Dim aabb2 As AABB
                    If (([next]._flags And ContactFlags.Filter) = ContactFlags.Filter) Then
                        If (other.IsStatic AndAlso body.IsStatic) Then
                            Dim c As Contact = [next]
                            [next] = c.GetNext
                            Me.Destroy(c)
                            Continue Do
                        End If
                        If body.IsConnected(other) Then
                            Dim contact3 As Contact = [next]
                            [next] = contact3.GetNext
                            Me.Destroy(contact3)
                            Continue Do
                        End If
                        If Not Me.ContactFilter.ShouldCollide(fixtureA, fixtureB) Then
                            Dim contact4 As Contact = [next]
                            [next] = contact4.GetNext
                            Me.Destroy(contact4)
                            Continue Do
                        End If
                        [next]._flags = ([next]._flags And Not ContactFlags.Filter)
                    End If
                    Dim proxyId As Integer = fixtureA._proxyId
                    Dim num2 As Integer = fixtureB._proxyId
                    Me._broadPhase.GetAABB(proxyId, aabb)
                    Me._broadPhase.GetAABB(num2, aabb2)
                    If Not AABB.TestOverlap(aabb, aabb2) Then
                        Dim contact5 As Contact = [next]
                        [next] = contact5.GetNext
                        Me.Destroy(contact5)
                    Else
                        [next].Update(Me.ContactListener)
                        [next] = [next].GetNext
                    End If
                End If
            Loop
        End Sub

        Friend Sub Destroy(ByVal c As Contact)
            Dim fixtureA As Fixture = c.GetFixtureA
            Dim fixtureB As Fixture = c.GetFixtureB
            Dim body As Body = fixtureA.GetBody
            Dim body2 As Body = fixtureB.GetBody
            If (c._manifold._pointCount > 0) Then
                Me.ContactListener.EndContact(c)
            End If
            If (c._prev IsNot Nothing) Then
                c._prev._next = c._next
            End If
            If (c._next IsNot Nothing) Then
                c._next._prev = c._prev
            End If
            If (c Is Me._contactList) Then
                Me._contactList = c._next
            End If
            If (c._nodeA.Prev IsNot Nothing) Then
                c._nodeA.Prev.Next = c._nodeA.Next
            End If
            If (c._nodeA.Next IsNot Nothing) Then
                c._nodeA.Next.Prev = c._nodeA.Prev
            End If
            If (c._nodeA Is body._contactList) Then
                body._contactList = c._nodeA.Next
            End If
            If (c._nodeB.Prev IsNot Nothing) Then
                c._nodeB.Prev.Next = c._nodeB.Next
            End If
            If (c._nodeB.Next IsNot Nothing) Then
                c._nodeB.Next.Prev = c._nodeB.Prev
            End If
            If (c._nodeB Is body2._contactList) Then
                body2._contactList = c._nodeB.Next
            End If
            Me._contactCount -= 1
        End Sub

        Friend Sub FindNewContacts()
            Me._broadPhase.UpdatePairs(Of Fixture)(Me._addPair)
        End Sub


        ' Properties
        Friend Property ContactFilter As IContactFilter
        Friend Property ContactListener As IContactListener

        ' Fields
        Private _addPair As Action(Of Fixture, Fixture)
        Friend _broadPhase As BroadPhase = New BroadPhase
        Friend _contactCount As Integer
        Friend _contactList As Contact
    End Class
End Namespace

