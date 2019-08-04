using System;
using System.Collections.Generic;
using UnityEngine;

public static class SkewerLogic {
    
    /// <summary>
    /// Throws the skewer
    /// </summary>
    /// <param name="nodes">All the nodes in the board</param>
    /// <param name="direction">the direction the player threw the skewer</param>
    /// <returns>a GameOverStatus if the player won or lose the level</returns>
    public static GameOverStatus ShootSkewer(List<List<Node>> nodes, Vector2 direction, SkewerThrower thrower) {

        int curDirX = 0;
        int curDirY = 0;
        
        if (Directions.East == direction) {
            curDirX = 1;
            curDirY = 0;
        }
        else if (Directions.West == direction) {
            curDirX = -1;
            curDirY = 0;
        }
        else if (Directions.North == direction) {
            curDirX = 0;
            curDirY = -1;
        }
        else if (Directions.South == direction) {
            curDirX = 0;
            curDirY = 1;
        }

        Entity player = null;
        int playerX = 0;
        int playerY = 0;

        List<Entity> ingredients = new List<Entity>();
        
        for (int row = 0; row < nodes.Count; row++) {
            for (int column = 0; column < nodes[row].Count; column++) {
                var node = nodes[row][column];
                if (node.entity != null) {
                    var entity = node.entity.GetComponent<Entity>();
                    if (entity != null) {
                        if (entity.Name == "Player") {
                            // found the player
                            player = entity;
                            playerX = column;
                            playerY = row;
                        } else {
                            ingredients.Add(entity);
                        }
                    }
                }
            }
        }
        
        if (player == null) throw new Exception("There wasn't a player on the board... you fucked up");

        List<Vector3> path = new List<Vector3>();
        path.Add(player.transform.position);

        int curPosX = playerX;
        int curPosY = playerY;
        
        List<Entity> skeweredIngredients = new List<Entity>();

        bool skeweredSelf = false;

        // while (is the next step on the board)
        while (curPosY + curDirY < nodes.Count && curPosY + curDirY >= 0 && curPosX + curDirX < nodes[curPosY].Count && curPosX + curDirX >= 0) {
            // this is the next step
            curPosX += curDirX;
            curPosY += curDirY;

            var node = nodes[curPosY][curPosX];
            if (node.entity != null) {
                var entity = node.entity.GetComponent<Entity>();
                skeweredIngredients.Add(entity);
            } else if (node.tile != null) {
                // handle angle changers
                if (node.ascii == "{" || node.ascii == "]") {
                    if ((curDirX <= 0 && curDirY >= 0) || (curDirX >= 0 && curDirY <= 0)) {
                        // flip
                        var x = curDirY;
                        var y = curDirX;
                        curDirX = x;
                        curDirY = y;
                        path.Add(node.transform.position);
                    } else {
                        // hit the back
                        break;
                    }
                } else if (node.ascii == "}" || node.ascii == "[") {
                    if ((curDirX >= 0 && curDirY >= 0) || (curDirX <= 0 && curDirY <= 0)) {
                        // flip
                        var x = -curDirY;
                        var y = -curDirX;
                        curDirX = x;
                        curDirY = y;
                        path.Add(node.transform.position);
                    } else {
                        // hit the back
                        break;
                    }
                }
            }

            if (curPosX == playerX && curPosY == playerY) skeweredSelf = true;

        }
        
        var lastValidNode = nodes[curPosY][curPosX];
        path.Add(lastValidNode.transform.position);
        
        thrower.Shoot(path, ingredients.ConvertAll(i => i.transform), 2);

        for (int i = 0; i < ingredients.Count; i++) {
            if (!skeweredIngredients.Contains(ingredients[i])) {
                return new GameOverStatus(GameOverReason.DIDNT_SKEWER_ALL_INGREDIENTS);
            }
        }
        
        if (skeweredSelf) return new GameOverStatus(GameOverReason.SKEWERD_YOURSELF);
        return new GameOverStatus(GameOverReason.SKEWERED_ALL_INGREDIENTS);
    }
}
