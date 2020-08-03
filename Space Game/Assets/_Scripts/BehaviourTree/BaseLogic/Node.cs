using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Node
{
    //Delegate that returns the state of the node
    public delegate NodeStates NodeReturn();

    //The current state of the node
    protected NodeStates m_nodeState;

    protected BlackBoard blackBoard;

    public NodeStates nodeState
    {
        get { return m_nodeState; }
    }

    //Node Constructor
    public Node()
    {

    }

    //Implementing classes using this method to evaluate the disired set of conditions
    public abstract NodeStates Evaluate();


}

public enum NodeStates
{
    RUNNING,
    SUCCESS,
    FAILURE
}
