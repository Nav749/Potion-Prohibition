public class Inventory
{
    private Item[] items;

    public void createInventory(Item[] items)
    {
        if (items == null)
        {
            this.items = items;
            for (int i = 0; i < items.Length; i++)
            {
                items[i].SetID(i);
            }
        }
        else
        {
            throw new System.Exception("Inventory already Created");
        }
    }

    public Item getIndex(int index)
    {
        return items[index];
    }

    public void incrmentIndex(int index)
    {
        items[index].incrmentAmount();
    }

    public void decrementIndex(int index)
    {
        items[index].decrementAmount();
    }

    public void resetIndex(int index)
    {
        items[index].resetAmount();
    }



}
