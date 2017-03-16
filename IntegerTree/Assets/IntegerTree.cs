//IAN CLINKENBEARD

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntegerTree : MonoBehaviour {

	public int inputValue;

	[System.Serializable]
	public class Node {

		public int value;
		public int treeDepth;
		public int index;
		public Node parent;
		public Node leftNeighbor;
		public Node rightNeighbor;
		public bool isLeftChild;

		//Constructor
		public Node (int _depth, int _index, bool _leftChild) {

			value = 1;
			treeDepth = _depth;
			index = _index;
			isLeftChild = _leftChild;
			parent = null;
			leftNeighbor = null;
			rightNeighbor = null;
		}

	};

	public List <Node> nodes = new List<Node>();

	void Start () {
	
		CreateTree ();
	}

	void CreateTree () {

		//Makes sure the list of nodes is empty
		nodes.Clear ();

		//Keeps track of the number of nodes that need to be created for the current row
		int nodesPerRow = 1;
		//Increments each time a new node is created
		int nodesIndex = 0;
		//Bool switches between true and false each time a node is created
		//Determines whether the new node is a left neighbor or not
		//False by default so it will be false for the leftmost node of the second row
		bool makeLeftNeighbor = false;

		//Goes through each row of nodes that needs be created, specified by the input value
		for (int row = 1; row <= inputValue; row++) {

			//Goes through each node that needs to be created for this row
			for (int i = 0; i < nodesPerRow; i++) {

				//Creates a new node and adds it to the list of nodes
				Node newNode = new Node (row, nodesIndex, makeLeftNeighbor);
				nodes.Add (newNode);

				//Increments nodes index and switches makeLeftNeighbor bool
				nodesIndex++;
				makeLeftNeighbor = !makeLeftNeighbor;
			}

			//Before moving onto the next row, doubles nodesPerRow
			//Since each row has double the amount of nodes as the row above
			nodesPerRow *= 2;
		}
	}

}
