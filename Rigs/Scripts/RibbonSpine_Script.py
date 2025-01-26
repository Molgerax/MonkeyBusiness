import maya.cmds as cmds

#type in your Loft's name
mySpine = 'C_RibbonSpine_LFT'

jntList = []

ctrlSize = 0.5
uValues = [0.0, 0.5, 0.999]

for index, jnt in enumerate(range(3)):
    cmds.select(clear = True)
    nr = index + 1
    
    #create joints
    spineJnt = cmds.joint(name = 'C_SpineSec{}_Skin_JNT'.format(nr))
    
    #create controls
    spineCtrl = cmds.circle(r = ctrlSize, nr = (0, 2, 0), name = 'C_SpineSec{}_CTRL'.format(nr), ch = 0)
    
    #parent joints to controls
    cmds.parent(spineJnt, spineCtrl)
    
    #create offset groups and parent the controls to them
    spineGrp = cmds.group(spineCtrl, name = 'C_SpineSec{}_GRP'.format(nr))
    cmds.setAttr('{}.overrideEnabled'.format(spineGrp), 1)
    cmds.setAttr('{}.overrideColor'.format(spineGrp), 20)
    
    jntList.append(spineGrp)
    
    #create a pointOnSurfaceInfo node
    spineInfo = cmds.createNode('pointOnSurfaceInfo', name = 'C_SpineSec{}_Info'.format(nr))
    
    #connect WorldSpace to inputSurface
    cmds.connectAttr('{}.worldSpace'.format(mySpine), '{}.inputSurface'.format(spineInfo))
    
    #connect Position to Translate
    cmds.connectAttr('{}.position'.format(spineInfo), '{}.translate'.format(spineGrp))
    
    #change Info attributes
    cmds.setAttr('{}.turnOnPercentage'.format(spineInfo), 1)
    cmds.setAttr('{}.parameterV'.format(spineInfo), 0.5)
    cmds.setAttr('{}.parameterU'.format(spineInfo), uValues[index])
    
for index, jnt in enumerate(range(3)):
    
    nr = index + 1
    
    #create a locator for each joint
    spineLoc = cmds.spaceLocator(name = 'C_SpineSec{}_LOC'.format(nr))
    
    #create a pointOnSurfaceInfo node
    spineInfo = cmds.createNode('pointOnSurfaceInfo', name = 'C_SpineSec{}_Info'.format(nr))
    
    #connect WorldSpace to inputSurface
    cmds.connectAttr('{}.worldSpace'.format(mySpine), '{}.inputSurface'.format(spineInfo))
    
    #connect Position to Translate
    cmds.connectAttr('{}.position'.format(spineInfo), '{}.translate'.format(spineLoc[0]))
    
    #change Info attributes
    cmds.setAttr('{}.turnOnPercentage'.format(spineInfo), 1)
    cmds.setAttr('{}.parameterV'.format(spineInfo), 0.5)
    cmds.setAttr('{}.parameterU'.format(spineInfo), uValues[index])
    
    #set up aim constraints
    if index != 2:
        cmds.aimConstraint(jntList[nr],  jntList[index], aim = (0.0, 1.0, 0.0), u = (1.0, 0.0, 0.0), wut = 'object', wuo = spineLoc[0])