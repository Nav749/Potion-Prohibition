using UnityEngine;

public class RecipeBook : MonoBehaviour
{
    [SerializeField] private Recipe[] recipes;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Recipe GetRecipe(int index) 
    { 
        return recipes[index];     
    }

    public int findRecipe(Recipe recipe)
    {
        for (int i = 0; i < recipes.Length; i++)
        {
            if (recipes[i] == recipe)
            {
                return i;
            }

        }
        return -1;
    }

    public bool checkBook(Recipe recipe) {
        for (int i = 0; i < recipes.Length; i++) {
            if (recipe == recipes[i]) { 
                return true;
            }
        }
        return false;
    }
}
