namespace GraphLoom.Mapper.RDF
{
    public class RDFMapperFactory
    {
        public static RDFGraphMapper CreateDefaultRDFMapper()
        {
            return new RDFGraphMapper(new TriplesAssembler());
        }

        public static RDFGraphMapper CreateRDFMapperWithAssembler(TriplesAssembler assembler)
        {
            return new RDFGraphMapper(assembler);
        }
    }
}
