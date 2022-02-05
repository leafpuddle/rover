node {
    stage ('Prepare environment') {
        git branch: 'dev', credentialsId: '0c9158e7-0eca-4c36-b49d-728fec708a2a', url: 'git.leafpuddle.dev:misc/rover.git'
        sh 'dotnet restore'
    }
    
    stage ('Build') {
        sh 'dotnet build'
    }
    
    stage ('Prepare Deployment') {
        sh 'ssh rover@garmr.faultybranches.net rm -rf /var/www/rover/dist'
        sh 'ssh rover@garmr.faultybranches.net mkdir -p /var/www/rover/dist'
        sh 'ssh rover@garmr.faultybranches.net chown -R rover:rover /var/www/rover/dist'
    }
    
    stage ('Deploy') {
        sh 'scp -r bin/Debug/net6.0/* rover@garmr.faultybranches.net:/var/www/rover/dist/'

        sh 'ssh rover@garmr.faultybranches.net sudo systemctl restart rover.service'
    }
}
