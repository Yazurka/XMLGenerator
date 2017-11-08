node {
	stage 'Checkout'
		checkout scm
	
	stage 'Restore'
		bat ("${nuget} restore XMLGenerator.sln")
	
	stage 'Build'	
		bat "\"${tool 'MSBuild'}\" XMLGenerator.sln /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
	
	
	stage 'Archive'
		archive 'XMLGenerator/bin/Release/**'

}
