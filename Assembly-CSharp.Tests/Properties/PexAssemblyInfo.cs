using Microsoft.Pex.Framework.Coverage;
using Microsoft.Pex.Framework.Creatable;
using Microsoft.Pex.Framework.Instrumentation;
using Microsoft.Pex.Framework.Settings;
using Microsoft.Pex.Framework.Validation;

// Microsoft.Pex.Framework.Settings
[assembly: PexAssemblySettings(TestFramework = "VisualStudioUnitTest")]

// Microsoft.Pex.Framework.Instrumentation
[assembly: PexAssemblyUnderTest("Assembly-CSharp")]
[assembly: PexInstrumentAssembly("UnityEngine.AudioModule")]
[assembly: PexInstrumentAssembly("UnityEditor")]
[assembly: PexInstrumentAssembly("Unity.TextMeshPro")]
[assembly: PexInstrumentAssembly("UnityEngine.ParticleSystemModule")]
[assembly: PexInstrumentAssembly("Assembly-CSharp-firstpass")]
[assembly: PexInstrumentAssembly("UnityEngine.PhysicsModule")]
[assembly: PexInstrumentAssembly("UnityEngine.CoreModule")]
[assembly: PexInstrumentAssembly("netstandard")]
[assembly: PexInstrumentAssembly("UnityEngine.UI")]
[assembly: PexInstrumentAssembly("UnityEngine.AnimationModule")]
[assembly: PexInstrumentAssembly("UnityEngine.AIModule")]

// Microsoft.Pex.Framework.Creatable
[assembly: PexCreatableFactoryForDelegates]

// Microsoft.Pex.Framework.Validation
[assembly: PexAllowedContractRequiresFailureAtTypeUnderTestSurface]
[assembly: PexAllowedXmlDocumentedException]

// Microsoft.Pex.Framework.Coverage
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "UnityEngine.AudioModule")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "UnityEditor")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "Unity.TextMeshPro")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "UnityEngine.ParticleSystemModule")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "Assembly-CSharp-firstpass")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "UnityEngine.PhysicsModule")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "UnityEngine.CoreModule")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "netstandard")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "UnityEngine.UI")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "UnityEngine.AnimationModule")]
[assembly: PexCoverageFilterAssembly(PexCoverageDomain.UserOrTestCode, "UnityEngine.AIModule")]

