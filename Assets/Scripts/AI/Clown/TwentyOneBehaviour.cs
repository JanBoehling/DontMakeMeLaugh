using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

public class TwentyOneBehaviour : BehaviorTreeBase
{
    [SerializeField]
    private TwentyOneData _gameData;

    public TwentyOneBehaviour() 
        : base()
    {
        
    }

    protected override Node SetupTree()
    {
        Node root = new Sequenzer(new List<Node>()
            {
                new Selector(new List<Node>()
                {
                    new Sequenzer(new List<Node>()
                    {
                        new CheckCardValueCount(_gameData, 21, 0, true), // CardValue > 21
                        new TaskOverTwentyOne(_gameData)
                    }),
                    new Sequenzer(new List<Node>()
                    {
                        new CheckCardValueCount(_gameData, 15, 3), // CardValue < 15 && CardCount <= 3
                        new TaskDrawCard(_gameData)
                    }),
                    new Sequenzer(new List<Node>()
                    {
                        new CheckCardValueCount(_gameData, 15, 3, true), // CardValue > 15 || CardCount > 3
                        new TaskDeclineNewCard(_gameData)
                    }),
                }),
            }
        );

        return root;
    }
}
