public struct Integer 
{ 
    public Integer(int _val) 
    { 
        mValue = _val; 
    } 

    public static implicit operator Integer(int _val) 
    { 
        return new Integer(_val); 
    } 

    public static implicit operator int(Integer _integer) 
    { 
        return _integer.mValue;
    } 

    public static int operator +(Integer a, Integer b) 
    {
		return (int)a + (int)b; 
    } 

    public static Integer operator +(int a, Integer b)
    {
	    return new Integer(a) + b;
    } 

    public static int operator -(Integer a, Integer b) 
    {
		return (int)a - (int)b; 
    } 

    public static Integer operator -(int a, Integer b)
    {
	    return new Integer(a) - b;
    }

	private readonly int mValue;
} 