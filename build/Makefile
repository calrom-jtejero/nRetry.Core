VERSION=0.3.2#

clean:
	rm -r ../artefacts || true
	rm -r ../*/*/obj || true
	rm -r ../*/*/bin || true

	mkdir -p ../artefacts/nuget

build: clean
	dotnet restore ../

# SpecFlow plugin must be built before the tests
	dotnet build -c Release --no-restore /p:Version=$(VERSION) ../src/nRetry.SpecFlowPlugin

	dotnet build -c Release --no-restore /p:Version=$(VERSION) ../test/

unit-tests-run:
	cd ../test && \
		dotnet test --no-build -c Release --logger:trx\;logfilename=../../artefacts/testResults/UnitTests.trx

nuget-create:
	dotnet pack ../src/nRetry.SpecFlowPlugin \
		/p:Version=$(VERSION) \
		--no-build \
		-c Release \
		-o ../../artefacts/nuget

	dotnet pack ../src/nRetry.SpecFlowPlugin \
		/p:Version=$(VERSION) \
		/p:NuspecFile=nRetry.SpecFlowPlugin.nuspec \
		--no-build \
		-c Release \
		-o ../../artefacts/nuget