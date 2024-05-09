namespace nivwer.EntitySerializer.Interfaces;

public interface IPropertyManager
{
    IPropertyAccessor Accessor { get; }
    IPropertyMapper Mapper { get; }
    
    void SetPropertyAccessor(IPropertyAccessor accessor);
    void SetPropertyMapper(IPropertyMapper mapper);
}