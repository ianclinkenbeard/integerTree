//IAN CLINKENBEARD

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntegerTree : MonoBehaviour {

	[Header("Input the number of tree levels that will be generated here.")]
	public int inputValue;

	///This class is used to store information for each node
	[System.Serializable]
	public class Node {

		///The node's data value
		public int value;
		///The level of the tree this node is in
		public int treeDepth;
		///This node's index in the nodes list
		public int index;
		///A reference to this node's parent node (is null if this is the root node)
		public Node parent;
		///A reference to this node's left neighbor (is null if it does not have one)
		public Node leftNeighbor;
		///A reference to this node's right neighbor (is null if it does not have one)
		public Node rightNeighbor;
		///If this node is the left child of its parent node, this is true
		public bool isLeftChild;

		//Constructor
		public Node (int _depth, int _index, bool _leftChild) {

			//Default value is 1
			value = 1;

			//Assigns given values for tree depth, list index and whether or not this is a left child
			treeDepth = _depth;
			index = _index;
			isLeftChild = _leftChild;

			//Parent / neighbor references are null by default
			parent = null;
			leftNeighbor = null;
			rightNeighbor = null;
		}
	};

	//A list containing each node
	//Nodes are arranged top to bottom, left to right
	//For example:
	/*
	1st level / root node: 0
	2nd level: 1, 2
	3rd level: 3, 4, 5, 6
	4th level: 7, 8, 9, 10, 11, 12, 13, 14
	*/
	public List <Node> nodes = new List<Node>();

	void Start () {
	
		//Creates the node tree
		CreateTree ();
		//Assigns values for all nodes
		AssignAllNodeValues ();
	}

	///Creates the node tree from the specified number of levels
	///Does not set the node values, parents, or neighbors
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

	//Assigns the correct value, parent and neighbors for each node in the nodes list
	void AssignAllNodeValues () {

		//Goes through each node, except for the root node
		//The root node will always have a value of 1 and does not have any neighbors or parent
		//So it does not need to be modified
		for (int i = 1; i < nodes.Count; i++) {

			//Assigns values for this node
			AssignNodeValues (nodes [i]);
		}
	}

	//Assigns the correct value, parent, and neighbors for the given node
	void AssignNodeValues (Node node) {

		//Makes sure this is not the root node and exits the function if it is
		if (node.treeDepth == 1) {
			return;
		}

		//Sets the reference to this node's parent

		if (node.isLeftChild) {

			//If this node is a left child, the index of its parent will be this node's index divided by two
			//I use multiplication instead of division to get the parent index for performance purposes
			int parentIndex = (int)((float)node.index * 0.5f);
			//Gets parent from list of nodes and assigns the reference to it
			node.parent = nodes [parentIndex];

		} else {

			//If this node is a right child, subtracts one from its index before dividing it by two to get parent index
			int parentIndex = (int)((float)((node.index - 1) * 0.5f));
			node.parent = nodes [parentIndex];
		}
			
		//If this node is at the bottom of the tree, does not set its neighbors as they will never be used
		if (node.treeDepth < inputValue) {

			//Sets left neighbor
			//If the node to the left of this node in the nodes list is on the same tree depth, it is its left neighbor
			if (nodes [node.index - 1].treeDepth == node.treeDepth) {
				node.leftNeighbor = nodes [node.index - 1];
			}

			//Sets right neighbor
			//If the node to the right of this node in the nodes list is on the same tree depth, it is its left neighbor
			if (nodes [node.index + 1].treeDepth == node.treeDepth) {
				node.rightNeighbor = nodes [node.index + 1];
			}
		}

		//Sets the value of this node to the value of its parent
		node.value = node.parent.value;

		//If this node is a left child and its parent has a left neighbor
		//Adds the value of the parent's left neighbor to this node's value
		if (node.isLeftChild && node.parent.leftNeighbor != null) {

			node.value += node.parent.leftNeighbor.value;
		}
		//If this node is a right child and its parent has a right neighbor
		//Adds the value of the parent's right neighbor to this node's value
		else if (!node.isLeftChild && node.parent.rightNeighbor != null) {

			node.value += node.parent.rightNeighbor.value;
		}
	}
}

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
