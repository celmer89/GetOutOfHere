/* ##########################################
#    classes for generating random maze basing on Prim's algorithm
#    https://en.wikipedia.org/wiki/Prim%27s_algorithm
#    author: Maciej Celmer
############################################*/


using System;

namespace MazeUtils{
	

// Class that represents one maze cell with neighbouring walls
public class Cell {
	
	// bool that states if cell was already visited by the algorithm
	private bool m_visited = false;

	// bools representing cell's walls
	private bool m_left = true;
	private bool m_right = true;
	private bool m_top = true;
	private bool m_bottom = true;
	
	public bool isVisited() {
		return m_visited;
	}
	public void setVisited(bool visited) {
		this.m_visited = visited;
	}
	public bool isLeft() {
		return m_left;
	}
	public void setLeft(bool left) {
		this.m_left = left;
	}
	public bool isRight() {
		return m_right;
	}
	public void setRight(bool right) {
		this.m_right = right;
	}
	public bool isTop() {
		return m_top;
	}
	public void setTop(bool top) {
		this.m_top = top;
	}
	public bool isBottom() {
		return m_bottom;
	}
	public void setBottom(bool bottom) {
		this.m_bottom = bottom;
	}
}


// Class that represents whole maze with random generating algorithm
public class Maze{
	
	private Cell[,] m_maze;
	private int m_width, m_height;
	private System.Random m_random = new Random();

	//############# Public methods ##########

	public void generateMaze (int w, int h) // generates maze with given dimmensions
	{	
		m_width = w;
		m_height = h;		
		m_maze = new Cell[m_width,m_height];
		for (int i=0; i<m_width; i++)
		{
			for (int j=0; j<m_height; j++)
			{
				this.m_maze[i,j] = new Cell();
			}
		}
		
		generate (0, 0);
	}


	public Cell getCell(int x, int y){

		if (m_maze.Length != 0){
			
			if (x < m_width && y < m_height){
				return m_maze [x, y];
			}

		}
		return null;
	}


	//############# Private methods ##########

	// recoursive core method for generating maze basing on Prim's algorithm
	private void generate(int x, int y) 
	{

		this.m_maze[x,y].setVisited(true);
		int next = m_random.Next (0, 99999999);
		
		for (int i = 0; i < 4; i++) {
			next = ((next + i) % 4);
			switch (next) {
			case 0:
				
				if ((y - 1) < 0) {
					break;
				} else if (m_maze[x,y - 1].isVisited() == true) {
					break;
				} else {
					this.m_maze[x,y].setTop(false);
					this.m_maze[x,y - 1].setBottom(false);
					generate(x, y - 1);
					break;
				}
				
			case 1:
				
				if ((x + 1) >= m_width) {
					break;
				} else if (m_maze[x + 1,y].isVisited() == true) {
					break;
				} else {
					this.m_maze[x,y].setRight(false);
					this.m_maze[x + 1,y].setLeft(false);
					generate(x + 1, y);
					break;
				}
				
			case 2:
				
				if ((y + 1) >= m_height) {
					break;
				} else if (m_maze[x,y + 1].isVisited() == true) {
					break;
				} else {
					this.m_maze[x,y].setBottom(false);
					this.m_maze[x,y + 1].setTop(false);
					generate(x, y + 1);
					break;
				}
				
			case 3:
				
				if ((x - 1) < 0) {
					break;
				} else if (m_maze[x - 1,y].isVisited() == true) {
					break;
				} else {
					this.m_maze[x,y].setLeft(false);
					this.m_maze[x - 1,y].setRight(false);
					generate(x - 1, y);
					break;
				}
			default:
				break;
			}
			
		}
		return;
	}
}

} // MazeUtils