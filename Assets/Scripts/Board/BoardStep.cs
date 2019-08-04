using System;
using System.Collections.Generic;

public class BoardStep {
    private List<List<Node>> nodes;

    public BoardStep() {
        nodes = new List<List<Node>>();
    }

    public BoardStep(List<List<Node>> nodes) {
        this.nodes = nodes;
    }

    public BoardStep AddRow(List<Node> row) {
        nodes.Add(row);
        return this;
    }

    public BoardStep AddRow() {
        nodes.Add(new List<Node>());
        return this;
    }

    public BoardStep AddToRow(int index, Node node) {
        nodes[index].Add(node);
        return this;
    }

    public Node Get(int row, int column) {
        return nodes[row][column];
    }

    public List<Node> GetRow(int row) {
        return nodes[row];
    }

    public BoardStep SetRow(int row, List<Node> nodes) {
        this.nodes[row] = nodes;
        return this;
    }

    public int Rows() {
        return nodes.Count;
    }

    public int Columns() {
        if (nodes.Count > 0) return nodes[0].Count;
        return 0;
    }

    public List<Node> this[int i] {
        get => GetRow(i);
        set => SetRow(i, value);
    }

    public override string ToString() {
        var strs = nodes.ConvertAll(col => String.Join("", col.ConvertAll(cell => cell.ToString())));
        strs.Reverse();
        return String.Join("\n", strs);
    }
}